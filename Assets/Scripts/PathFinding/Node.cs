using UnityEngine;

public class Node {

	public bool walkable;
	public Vector3 worldPosition;

	/*
	 * Un node est une des divisions de la grid sur laquelle le character peut se déplacer
	 */
	public Node(bool walkable, Vector3 worldPosition)
	{
		this.walkable = walkable;
		this.worldPosition = worldPosition;
	}
}
