﻿using UnityEngine;
using System.Collections;

public class Mouse : Character
{
	public Transform target;
	Vector3[] path;
	int targetIndex;

	void Start()
	{
		PathRequestManager.RequestPath(transform.position, target.position, OnPathFound);
	}

	public void OnPathFound(Vector3[] newPath, bool pathSuccessful)
	{
		if(pathSuccessful)
		{
			path = newPath;
			StopCoroutine("FollowPath");
			StartCoroutine("FollowPath");
		}
	}

	IEnumerator FollowPath()
	{
		Vector3 currentWaypoint = path[0];

		while(true)
		{
			if(transform.position == currentWaypoint)
			{
				targetIndex++;
				if(targetIndex >= path.Length)
				{
					yield break; // Exit the coroutine
				}
				currentWaypoint = path[targetIndex];
			}

			transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, speed);

			yield return null; // Move to the next frame
		}
	}

	/*void Update()
	{
		
		POOR PATHFINDING
		if (Vector3.SqrMagnitude(targetPositions [currentPosition].transform.position - transform.position) <= 0.02)
		{
			currentPosition = (currentPosition + 1) % targetPositions.Count;
		}

		transform.position = Vector3.Lerp (transform.position, targetPositions [currentPosition].transform.position, (Mathf.Sin(speed * Time.time) + 0.01f) / 10.0f);
	}*/

}