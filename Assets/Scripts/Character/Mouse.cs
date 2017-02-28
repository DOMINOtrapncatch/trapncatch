using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Mouse : Character
{
	public PathFinding pathFinding;
	public List<GameObject> targetPositions = new List<GameObject>();
	int currentPosition = 0;

	void Update()
	{
		if (Vector3.SqrMagnitude(targetPositions [currentPosition].transform.position - transform.position) <= 0.02)
		{
			currentPosition = (currentPosition + 1) % targetPositions.Count;
		}

		transform.position = Vector3.Lerp (transform.position, targetPositions [currentPosition].transform.position, (Mathf.Sin(speed * Time.time) + 0.01f) / 10.0f);
	}

}