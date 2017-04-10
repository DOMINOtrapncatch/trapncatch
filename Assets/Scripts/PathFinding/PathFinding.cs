using UnityEngine;
using System.Collections.Generic;
using System;

public class PathFinding : MonoBehaviour {
	
	Grid grid;

	void Awake()
	{
		grid = GetComponent <Grid> ();
	}

	public void FindPath(PathRequest request, Action<PathResult> callback)
	{
		Vector3[] waypoints = new Vector3[0];
		bool pathSuccess = false;

		// Convert positions to nodes
		Node startNode = grid.NodeFromWorldPoint (request.pathStart);
		Node targetNode = grid.NodeFromWorldPoint (request.pathEnd);

		if(startNode.walkable && targetNode.walkable)
		{
			// --- A partir de la, il faut comprendre le reserch A* parce que je ne vais pas expliquer
			Heap<Node> openSet = new Heap<Node>(grid.MaxSize);
			HashSet<Node> closedSet = new HashSet<Node>();

			openSet.Add (startNode);

			while(openSet.Count > 0)
			{
				Node currentNode = openSet.RemoveFirst();

				closedSet.Add (currentNode);

				if (currentNode == targetNode)
				{
					pathSuccess = true;
					break;
				}

				foreach(Node neighbor in grid.GetNeighbors (currentNode))
				{
					if (!neighbor.walkable || closedSet.Contains (neighbor))
						continue;

					int newGCostToNeighbor = currentNode.gCost + GetDistance(currentNode, neighbor);
					newGCostToNeighbor += neighbor.movementPenalty; // Allow preference for fast surfaces
					if(newGCostToNeighbor<neighbor.gCost || !openSet.Contains (neighbor))
					{
						neighbor.gCost = newGCostToNeighbor;
						neighbor.hCost = GetDistance(neighbor, targetNode);
						neighbor.parent = currentNode;

						if (!openSet.Contains(neighbor))
							openSet.Add(neighbor);
						else
							openSet.UpdateItem(neighbor);
					}
				}
			}
			// --- Fin du A*
		}

		if(pathSuccess)
		{
			waypoints = RetracePath(startNode, targetNode);
			pathSuccess = waypoints.Length > 0;
		}

		callback(new PathResult(waypoints, pathSuccess, request.callback));
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
