using UnityEngine;
using System.Collections;

public class Grid : MonoBehaviour {

	public LayerMask unwalkableMask;
	public Vector2 gridWorldSize;
	public float nodeRadius;
	Node[,] grid;

	float nodeDiameter;
	int gridSizeX, gridSizeY;

	void Start()
	{
		// Initialisation des variables
		nodeDiameter = nodeRadius * 2;
		gridSizeX = Mathf.RoundToInt (gridWorldSize.x / nodeDiameter);
		gridSizeY = Mathf.RoundToInt (gridWorldSize.y / nodeDiameter);

		// Creation de la grille
		CreateGrid ();
	}

	void CreateGrid()
	{
		// Nouveau node
		grid = new Node[gridSizeX, gridSizeY];
		// On obtient la position du coin bas gauche de la map par rapport au centre et sous la forme d'un vecteur
		Vector3 worldBottomLeft = transform.position - Vector3.right * gridWorldSize.x / 2 - Vector3.forward * gridWorldSize.y / 2;

		// Pour chaque division de la grille
		for (int x = 0; x < gridSizeX; x++)
		{
			for (int y = 0; y < gridSizeY; y++)
			{
				// On obtient la position du node par rapport à la position du coin bas gauche de la grille
				Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.forward * (y * nodeDiameter + nodeRadius);
				// On check si le node colisionne un objet qui a le mask "unwalkable"
				bool walkable = !(Physics.CheckSphere (worldPoint, nodeRadius, unwalkableMask));
				// On ajoute le node à la grille
				grid [x, y] = new Node (walkable, worldPoint);
			}
		}
	}

	/*
	 * DEBUG - Affichage de la grille de nodes et de leur etat
	 */
	void OnDrawGizmos()
	{
		Gizmos.DrawWireCube (transform.position, new Vector3(gridWorldSize.x, 1, gridWorldSize.y));

		if(grid != null)
		{
			foreach(Node n in grid)
			{
				Gizmos.color = n.walkable ? Color.white : Color.red;
				Gizmos.DrawCube (n.worldPosition, Vector3.one * (nodeDiameter - .1f));
			}
		}
	}
}