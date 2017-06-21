using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat : Character
{
	public int smartMouseKillCount { get; private set; }
	public List<Character> aroundMouses = new List<Character>();

	[Header("Pathfinding Settings")]
	public bool isPathfindingActive = false;
	public CatType catType = CatType.NORMAL;

	const float minPathUpdateTime = .2f;
	const float pathUpdateMoveThreshold = .5f;

	List<Transform> targets = new List<Transform>();
	public float turnSpeed = 3;
	public float turnDst = 5;
	public float stoppingDst = 10;

	int targetIndex;
	Path path;

	public void InitTargets(int spawnID, List<Transform> targets)
	{
		this.targets = targets;
		targetIndex = spawnID;
		transform.position = targets[targetIndex].position;
	}

	public void StartUpdating()
	{
		StartCoroutine(UpdatePath());
	}

	public void OnPathFound(Vector3[] waypoints, bool pathSuccessful)
	{
		if (pathSuccessful)
		{
			// Get success path
			path = new Path(waypoints, transform.position, turnDst, stoppingDst);

			StopCoroutine("FollowPath"); // In case coroutine alwready exists
			StartCoroutine("FollowPath");
		}
	}

	IEnumerator UpdatePath()
	{
		// Handle level loading trouble maker
		if (Time.timeSinceLevelLoad < .3f)
			yield return new WaitForSeconds(.3f);

        while (gameObject != null) // While alive
		{
			if (isPathfindingActive)
			{
				if (targets.Count > 0 && targetIndex > 0) // If it has targets and if they are reachable
				{
					PathRequestManager.RequestPath(new PathRequest(transform.position, targets[targetIndex].position, OnPathFound));

					float sqrMoveThreshold = pathUpdateMoveThreshold * pathUpdateMoveThreshold;
					Vector3 targetPosOld = targets[targetIndex].position;

					while (targetIndex >= 0) // While you don't encounter any mouses, keep moving points to points
					{
						// Update only if moved a certain dist (this is here for performance issues)
						if ((targets[targetIndex].position - targetPosOld).sqrMagnitude > sqrMoveThreshold)
						{
							PathRequestManager.RequestPath(new PathRequest(transform.position, targets[targetIndex].position, OnPathFound));
							targetPosOld = targets[targetIndex].position;
						}

						yield return new WaitForSeconds(minPathUpdateTime);
					}
				}
				else if (aroundMouses.Count > 0) // If you detected some mouses
				{
					PathRequestManager.RequestPath(new PathRequest(transform.position, aroundMouses[0].transform.position, OnPathFound));

					float sqrMoveThreshold = pathUpdateMoveThreshold * pathUpdateMoveThreshold;
					Vector3 targetPosOld = aroundMouses[0].transform.position;

					while (aroundMouses.Count != 0) // While you have mouses in your sight
					{
						// Update only if moved a certain dist (this is here for performance issues)
						if ((aroundMouses[0].transform.position - targetPosOld).sqrMagnitude > sqrMoveThreshold)
						{
							PathRequestManager.RequestPath(new PathRequest(transform.position, aroundMouses[0].transform.position, OnPathFound));
							targetPosOld = aroundMouses[0].transform.position;
						}

						yield return new WaitForSeconds(minPathUpdateTime);
					}
				}
				else
				{
					DecideNextMove(); // Set the direction to choose if nothing above works
					yield return new WaitForSeconds(minPathUpdateTime);
				}
			}
			else
			{
				yield return new WaitForSeconds(minPathUpdateTime);
			}
		}
	}

	IEnumerator FollowPath()
	{
		bool followingPath = true;
		int pathIndex = 0;
		transform.LookAt(path.lookPoints[0]);

		float speedPercent = 1;

		while (followingPath && isPathfindingActive)
		{
			Vector2 pos2D = new Vector2(transform.position.x, transform.position.z);
			while (path.turnBoundaries[pathIndex].HasCrossedLine(pos2D))
			{
				if (pathIndex == path.finishLineIndex)
				{
					followingPath = false;
					break;
				}
				else
				{
					pathIndex++;
				}
			}

			if (followingPath)
			{
				if (pathIndex >= path.slowDownIndex && stoppingDst > 0)
				{
					speedPercent = Mathf.Clamp01(path.turnBoundaries[path.finishLineIndex].DistanceFromPoint(pos2D) / stoppingDst);
					followingPath = !(speedPercent < 0.2f);
				}

				Quaternion targetRotation = Quaternion.LookRotation(path.lookPoints[pathIndex] - transform.position);
				transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * turnSpeed);
				transform.Translate(Vector3.forward * Time.deltaTime * Speed * speedPercent, Space.Self);
			}

			// Priority to the nearest enemy
			if (targetIndex != -1 && catType == CatType.NORMAL && aroundMouses.Count > 0)
			{
				targetIndex = -1;
				break;
			}

			yield return null; // Move to the next frame
		}

		DecideNextMove();
	}

	void DecideNextMove()
	{
		// Decide of the next move
		switch (catType)
		{
			case CatType.NORMAL:
				// If there are enemies near me, follow it. Else, go somewere you know.
				transform.position = targets[Random.Range(1, targets.Count - 1)].position;
				break;
		}
	}

	public void OnDrawGizmos()
	{
		if (path != null)
			path.DrawWithGizmos();
	}

    public override void OnEnemyKill(Character enemy)
    {
		Mouse mouse = enemy.gameObject.GetComponent<Mouse>();
		if (mouse != null && mouse.mouseType == MouseType.SMART)
			smartMouseKillCount++;

		base.OnEnemyKill(enemy);
	}

	/*
     * TRIGGERS PART
     */

	public override void AddAroundEnemy(Character enemy)
	{
		if (enemy.GetComponent<Mouse>() != null)
			aroundMouses.Add(enemy);

		base.AddAroundEnemy(enemy);
	}

	public override void RemoveAroundEnemy(Character enemy)
	{
		if (aroundMouses.Contains(enemy))
			aroundMouses.Remove(enemy);

		base.RemoveAroundEnemy(enemy);
	}
}

public enum CatType
{
	NORMAL
}