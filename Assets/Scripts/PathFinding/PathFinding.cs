using UnityEngine;
using System.Collections.Generic;
using System;
using System.Net.Mime;

public class PathFinding : MonoBehaviour {

	public Transform seeker, target;

	Grid grid;

	void Awake()
	{
		grid = GetComponent <Grid> ();
	}

	void Update()
	{
		//FindPath (seeker.position, target.position);
	}

	void FindPath(Vector3 startPos, Vector3 targetPos)
	{
		// Convert positions to nodes
		Node startNode = grid.NodeFromWorldPoint (startPos);
		Node targetNode = grid.NodeFromWorldPoint (targetPos);

		// --- A partir de la, il faut comprendre le reserch A* parce que je ne vais pas expliquer
		List<Node> openSet = new List<Node> ();
		HashSet<Node> closedSet = new HashSet<Node> ();

		openSet.Add (startNode);

		while(openSet.Count > 0)
		{
			Node currentNode = openSet [0];
			for (int i = 1; i < openSet.Count; i++)
			{
				if (openSet [i].fCost < currentNode.fCost || openSet[i].fCost == currentNode.fCost)
					if(openSet[i].hCost < currentNode.hCost)
						currentNode = openSet [i];
			}

			openSet.Remove (currentNode);
			closedSet.Add (currentNode);

			if (currentNode == targetNode)
			{
				RetracePath (startNode, targetNode);
				return;
			}

			foreach(Node neighbor in grid.GetNeighbors (currentNode))
			{
				if (!neighbor.walkable || closedSet.Contains (neighbor))
					continue;

				int newGCostToNeighbor = currentNode.gCost + GetDistance (currentNode, neighbor);
				if(newGCostToNeighbor < neighbor.gCost || !openSet.Contains (neighbor))
				{
					neighbor.gCost = newGCostToNeighbor;
					neighbor.hCost = GetDistance (neighbor, targetNode);
					neighbor.parent = currentNode;

					if (!openSet.Contains (neighbor))
						openSet.Add (neighbor);
				}
			}
		}
		// --- Fin du A*
	}

	void RetracePath(Node startNode, Node endNode)
	{
		List<Node> path = new List<Node> ();
		Node currentNode = endNode;

		while(currentNode != startNode)
		{
			path.Add (currentNode);
			currentNode = currentNode.parent;
		}

		path.Reverse ();

		grid.path = path;
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
