using UnityEngine;
using System.Collections.Generic;
using System;
using System.Diagnostics;
using System.Collections;

public class PathFinding : MonoBehaviour {

	// NO MORE NEEDED public Transform seeker, target;

	PathRequestManager requestManager;
	Grid grid;

	void Awake()
	{
		requestManager = GetComponent<PathRequestManager>();
		grid = GetComponent <Grid> ();
	}

	/*
	NO MORE NEEDED
	void Update()
	{
		if(Input.GetButtonDown("Jump"))
		{
			FindPath(seeker.position, target.position);
		}
	}
	*/

	public void StartFindPath(Vector3 startPos, Vector3 targetPos)
	{
		StartCoroutine(FindPath(startPos, targetPos));
	}

	IEnumerator FindPath(Vector3 startPos, Vector3 targetPos)
	{
		Stopwatch sw = new Stopwatch();
		sw.Start();

		Vector3[] wapoints = new Vector3[0];
		bool pathSuccess = false;

		// Convert positions to nodes
		Node startNode = grid.NodeFromWorldPoint (startPos);
		Node targetNode = grid.NodeFromWorldPoint (targetPos);

		if(startNode.walkable && targetNode.walkable)
		{
			// --- A partir de la, il faut comprendre le reserch A* parce que je ne vais pas expliquer
			Heap<Node> openSet = new Heap<Node>(grid.MaxSize);
			HashSet<Node> closedSet = new HashSet<Node>();

			openSet.Add (startNode);

			while(openSet.Count > 0)
			{
				Node currentNode = openSet.RemoveFirst(); // C'est beau :')
				/*
				Node currentNode = openSet [0];
				for (int i = 1; i < openSet.Count; i++)
				{
				if (openSet [i].fCost < currentNode.fCost || openSet[i].fCost == currentNode.fCost)
					if(openSet[i].hCost < currentNode.hCost)
						currentNode = openSet [i];
				}

				openSet.Remove (currentNode);
				*/

				closedSet.Add (currentNode);

				if (currentNode == targetNode)
				{
					sw.Stop();
					print("Path found: " + sw.ElapsedMilliseconds + "ms");
					pathSuccess = true;
					break;
				}

				foreach(Node neighbor in grid.GetNeighbors (currentNode))
				{
					if (!neighbor.walkable || closedSet.Contains (neighbor))
						continue;

					int newGCostToNeighbor = currentNode.gCost + GetDistance(currentNode, neighbor);
					if(newGCostToNeighbor<neighbor.gCost || !openSet.Contains (neighbor))
					{
						neighbor.gCost = newGCostToNeighbor;
						neighbor.hCost = GetDistance(neighbor, targetNode);
						neighbor.parent = currentNode;

						if (!openSet.Contains (neighbor))
							openSet.Add (neighbor);
					}
				}
			}
			// --- Fin du A*
		}

		yield return null; // Wait for 1 frame before returning

		if(pathSuccess)
			wapoints = RetracePath(startNode, targetNode);

		requestManager.FinishedProcessingPath(wapoints, pathSuccess);
	}

	Vector3[] RetracePath(Node startNode, Node endNode)
	{
		List<Node> path = new List<Node> ();
		Node currentNode = endNode;

		while(currentNode != startNode)
		{
			path.Add (currentNode);
			currentNode = currentNode.parent;
		}

		Vector3[] waypoints = SimplifyPath(path);
		Array.Reverse(waypoints);
		return waypoints;
	}

	Vector3[] SimplifyPath(List<Node> path)
	{
		List<Vector3> waypoints = new List<Vector3>();
		Vector2 directionOld = Vector2.zero;

		for (int i = 1; i < path.Count; i++)
		{
			Vector2 directionNew = new Vector2(path[i - 1].gridX - path[i].gridX, path[i - 1].gridY - path[i].gridY);

			if(directionNew != directionOld)
				waypoints.Add(path[i].worldPosition);

			directionOld = directionNew;
		}

		return waypoints.ToArray();
	}

	/*
	 * Permet d'obtenir la distance entre deux nodes
	 */
	int GetDistance(Node nodeA, Node nodeB)
	{
		int distX = Math.Abs (nodeA.gridX - nodeB.gridX);
		int distY = Math.Abs (nodeA.gridY - nodeB.gridY);

		if (distX > distY)
			return 14 * distY + 10 * (distX - distY);
		return 14 * distX + 10 * (distY - distX);
	}
}
