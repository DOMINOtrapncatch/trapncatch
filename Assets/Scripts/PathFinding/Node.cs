using UnityEngine;

public class Node {

	public bool walkable;
	public Vector3 worldPosition;
	public int gridX, gridY;

	public int gCost, hCost;
	public int fCost { get { return gCost + hCost; } }
	public Node parent;

	/*
	 * Un node est une des divisions de la grid sur laquelle le character peut se déplacer
	 */
	public Node(bool walkable, Vector3 worldPosition, int gridX, int gridY)
	{
		this.walkable = walkable;
		this.worldPosition = worldPosition;
		this.gridX = gridX;
		this.gridY = gridY;
	}
}
