using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse : Character
{
	MouseManager mouseManager;
	public List<Character> nearCats = new List<Character>();
	public List<Character> aroundCats = new List<Character>();

	[Header("Pathfinding Settings")]
	public bool isPathfindingActive = false;
	public MouseType mouseType = MouseType.NORMAL;

	const float minPathUpdateTime = .2f;
	const float pathUpdateMoveThreshold = .5f;

    [HideInInspector]
	public List<Transform> targets = new List<Transform>();
	public float turnSpeed = 3;
	public float turnDst = 5;
	public float stoppingDst = 10;

	int targetIndex;
	Path path;

	void Start()
	{
		// Update randomizer seed
		Random.InitState(System.DateTime.Now.Millisecond);

		// Get Mouse Manager
		mouseManager = GameObject.Find("MouseManager").GetComponent<MouseManager>();
	}

	public void InitTargets(int spawnID, List<Transform> targets)
	{
		this.targets = targets;
		transform.position = targets[spawnID].position;
        do {
            targetIndex = Random.Range(0, targets.Count - 1);
        } while (targetIndex == spawnID);
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

			StopCoroutine("FollowPath"); // In mousese coroutine alwready exists
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
				if (targets.Count > 0 && targetIndex >= 0) // If it has targets and if they are reachable
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
				else if (aroundCats.Count > 0 && mouseType == MouseType.SUICIDE) // If you detected some cats and you're suicidal
				{
					PathRequestManager.RequestPath(new PathRequest(transform.position, aroundCats[0].transform.position, OnPathFound));

					float sqrMoveThreshold = pathUpdateMoveThreshold * pathUpdateMoveThreshold;
					Vector3 targetPosOld = aroundCats[0].transform.position;

					while (aroundCats.Count != 0) // While you have cats in your sight
					{
						// Update only if moved a certain dist (this is here for performance issues)
						if ((aroundCats[0].transform.position - targetPosOld).sqrMagnitude > sqrMoveThreshold)
						{
							PathRequestManager.RequestPath(new PathRequest(transform.position, aroundCats[0].transform.position, OnPathFound));
							targetPosOld = aroundCats[0].transform.position;
						}

						yield return new WaitForSeconds(minPathUpdateTime);
					}
				}
				else if (aroundCats.Count > 0 && mouseType == MouseType.SMART) // If you detected some cats and you're smart
				{
					PathRequestManager.RequestPath(new PathRequest(transform.position, aroundCats[0].transform.position, OnPathFound));

					float sqrMoveThreshold = pathUpdateMoveThreshold * pathUpdateMoveThreshold;
					Vector3 targetPosOld = aroundCats[0].transform.position;

					while (aroundCats.Count != 0) // While you have cats in your sight
					{
						int newPos = GetBestSpotToGetAwayFromCat();

						// Update only if moved a certain dist (this is here for performance issues)
						if ((targets[newPos].position - targetPosOld).sqrMagnitude > sqrMoveThreshold)
						{
							PathRequestManager.RequestPath(new PathRequest(transform.position, targets[newPos].position, OnPathFound));
							targetPosOld = targets[newPos].position;
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
				transform.Translate(Vector3.forward * Time.deltaTime * Speed * 10 * speedPercent, Space.Self);
			}

			// Priority to the nearest enemy
			if (targetIndex != -1 && (mouseType == MouseType.SUICIDE || mouseType == MouseType.SMART) && aroundCats.Count > 0)
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
		switch (mouseType)
		{
			case MouseType.SMART:
			case MouseType.SUICIDE:
				// If there are no enemies, go somewere you know.
				if (aroundCats.Count == 0)
					targetIndex = Random.Range(0, targets.Count - 1);
				break;

			case MouseType.DUMB:
				// Go to next point
				targetIndex = (targetIndex + 1) % targets.Count;
				break;

			case MouseType.NORMAL:
				// Disappear
				Death(null);
				break;
		}
	}

	int GetBestSpotToGetAwayFromCat()
	{
		int bestSpot = 0;
		float bestValue = Vector3.Distance(targets[0].position, aroundCats[0].transform.position) - Vector3.Distance(targets[0].position, transform.position);

		for (int i = 1; i < targets.Count; i++)
		{
			float newValue = Vector3.Distance(targets[i].position, aroundCats[0].transform.position) - Vector3.Distance(targets[i].position, transform.position);
			if (newValue > bestValue)
			{
				bestSpot = i;
				bestValue = newValue;
			}
		}

		return bestSpot;
	}

	public override void Death(GameObject source)
	{
		mouseManager.Remove(gameObject);
		base.Death(source);
	}

	public void OnDrawGizmos()
	{
		if (path != null)
			path.DrawWithGizmos();
	}

	/*
     * TRIGGERS PART
     */

	public override void AddAroundEnemy(Character enemy)
	{
		if (enemy.GetComponent<Cat>() != null)
			aroundCats.Add(enemy);

		base.AddAroundEnemy(enemy);
	}

	public override void AddNearEnemy(Character enemy)
	{
		if (enemy.GetComponent<Cat>() != null)
			nearCats.Add(enemy);

		base.AddNearEnemy(enemy);
	}

	public override void RemoveAroundEnemy(Character enemy)
	{
		if (aroundCats.Contains(enemy))
			aroundCats.Remove(enemy);

		base.RemoveAroundEnemy(enemy);
	}

	public override void RemoveNearEnemy(Character enemy)
	{
		if (nearCats.Contains(enemy))
			nearCats.Remove(enemy);

		base.RemoveNearEnemy(enemy);
	}
}

public enum MouseType
{
	NORMAL, DUMB, SMART, SUICIDE
}