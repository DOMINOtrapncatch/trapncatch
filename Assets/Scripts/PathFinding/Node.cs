using UnityEngine;

public class Node : IHeapItem<Node> {

	public bool walkable;
	public Vector3 worldPosition;
	public int gridX, gridY;
	public int movementPenalty;

	public int gCost, hCost;
	public int fCost { get { return gCost + hCost; } }
	public Node parent;

	/*
	 * Un node est une des divisions de la grid sur laquelle le character peut se déplacer
	 */
	public Node(bool walkable, Vector3 worldPosition, int gridX, int gridY, int movementPenalty)
	{
		this.walkable = walkable;
		this.worldPosition = worldPosition;
		this.gridX = gridX;
		this.gridY = gridY;
		this.movementPenalty = movementPenalty;
	}


	//  +----------
	//  | Heap Implementation
	//  +----------

	public int HeapIndex { get; set; }

	public int CompareTo(Node nodeToCompare)
	{
		int compare = fCost.CompareTo(nodeToCompare.fCost);

		if (compare == 0)
			compare = hCost.CompareTo(nodeToCompare.hCost);

		return -compare;
	}
}
