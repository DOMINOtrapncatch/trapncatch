using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Mouse : Character
{
	public int damageValue = 10;

	[Header("Pathfinding Settings")]
	public MouseIA mouseIA = MouseIA.NORMAL;
    public MouseType mouseType = MouseType.LVL1;

	const float minPathUpdateTime = .2f;
	const float pathUpdateMoveThreshold = .5f;

	public List<Transform> targets = new List<Transform>();
	public bool randomTarget = true;
	public float turnSpeed = 3;
	public float turnDst = 5;
	public float stoppingDst = 10;

	int targetIndex;
	Path path;

	[Header("GUI Settings")]
	public Image healthBar;

	[Header("Particle Effects")]
	public GameObject deathPrefab;
	public GameObject explosionPrefab;

	void Start()
	{
		// Update randomizer seed
		Random.InitState(System.DateTime.Now.Millisecond);

		// Init start and target based on random or not
		if(randomTarget && mouseIA != MouseIA.SUICIDE)
		{
			targetIndex = Random.Range(0, targets.Count - 1);

			int startIndex;
			if(targets.Count > 1)
			{
				do
				{
					startIndex = Random.Range(0, targets.Count - 1);
				}
				while (startIndex == targetIndex);
			}
			else
			{
				startIndex = 0;
			}

			transform.position = targets[startIndex].position;
		}
		else
		{
			targetIndex = 1;
			transform.position = targets[0].position;
		}

		StartCoroutine(UpdatePath());
	}

	void Update()
	{
		healthBar.fillAmount = this.life / this.maxLife;
	}

	public void OnPathFound(Vector3[] waypoints, bool pathSuccessful)
	{
		if(pathSuccessful)
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
		
		PathRequestManager.RequestPath(new PathRequest(transform.position, targets[targetIndex].position, OnPathFound));

		float sqrMoveThreshold = pathUpdateMoveThreshold * pathUpdateMoveThreshold;
		Vector3 targetPosOld = targets[targetIndex].position;

		while(true)
		{
			yield return new WaitForSeconds(minPathUpdateTime);

			// Update only if moved a certain dist (this is here for performance issues)
			if((targets[targetIndex].position - targetPosOld).sqrMagnitude > sqrMoveThreshold)
			{
				PathRequestManager.RequestPath(new PathRequest(transform.position, targets[targetIndex].position, OnPathFound));
				targetPosOld = targets[targetIndex].position;
			}
		}
	}

	IEnumerator FollowPath()
	{
		bool followingPath = true;
		int pathIndex = 0;
		transform.LookAt(path.lookPoints[0]);

		float speedPercent = 1;

		while (followingPath)
		{
			Vector2 pos2D = new Vector2(transform.position.x, transform.position.z);
			while(path.turnBoundaries[pathIndex].HasCrossedLine(pos2D))
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
				if(pathIndex >= path.slowDownIndex && stoppingDst > 0)
				{
					speedPercent = Mathf.Clamp01(path.turnBoundaries[path.finishLineIndex].DistanceFromPoint(pos2D) / stoppingDst);
					followingPath = !(speedPercent < 0.2f);
				}

				Quaternion targetRotation = Quaternion.LookRotation(path.lookPoints[pathIndex] - transform.position);
				transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * turnSpeed);
				transform.Translate(Vector3.forward * Time.deltaTime * speed * speedPercent, Space.Self);
			}

			yield return null; // Move to the next frame
		}

		// Decide of the next move
		switch(mouseIA)
		{
			case MouseIA.SUICIDE:
				// Keep following
				targetIndex = randomTarget ? Random.Range(1, targets.Count - 1) : 1;
				break;

			case MouseIA.CIRCLE:
				// Go to next point
				targetIndex = (targetIndex + 1) % targets.Count;
				break;

			case MouseIA.NORMAL:
				// Disappear
                Destroy(gameObject);
				break;
		}
	}

	public void OnDrawGizmos()
	{
		if(path != null)
		{
			path.DrawWithGizmos();
		}
	}

	public void TryExplodeOn(Cat enemy)
	{
		if(mouseIA == MouseIA.SUICIDE && enemy != null)
		{
			enemy.StartCoroutine(ExplodeOn(enemy));
			Destroy(gameObject);
		}
	}

	IEnumerator ExplodeOn(Cat enemy)
	{
		//Deal damage to enemy
		enemy.Damage(damageValue);

		// Handle animation
		GameObject explosionEffect = (GameObject)Instantiate(explosionPrefab, transform.position, explosionPrefab.transform.rotation);
		yield return new WaitForSeconds(2.0f);
		Destroy(explosionEffect);
	}

	/*void Update()
	{
		[POOR PATHFINDING]
		=> I KEEP THIS IF WE WANT TO RE-IMPLEMENT IT (BUT DAMN CHANGE THE CALCULATION IT IS BAADDDD)

		if (Vector3.SqrMagnitude(targetPositions [currentPosition].transform.position - transform.position) <= 0.02)
		{
			currentPosition = (currentPosition + 1) % targetPositions.Count;
		}

		transform.position = Vector3.Lerp (transform.position, targetPositions [currentPosition].transform.position, (Mathf.Sin(speed * Time.time) + 0.01f) / 10.0f);
	}*/

}

public enum MouseIA
{
	NORMAL, SUICIDE, CIRCLE
}

public enum MouseType
{
    LVL1, LVL2, LVL3
}