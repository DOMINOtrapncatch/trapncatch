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

	public Node NodeFromWorldPoint(Vector3 worldPosition)
	{
		// Initialisation des pourcentages representant la position sur la grille
		float percentX = (worldPosition.x + gridWorldSize.x / 2) / gridWorldSize.x;
		float percentY = (worldPosition.z + gridWorldSize.y / 2) / gridWorldSize.y;
		// Stay into the boundaries
		percentX = Mathf.Clamp01 (percentX);
		percentY = Mathf.Clamp01 (percentY);

		// On obtient les positions du node
		int x = Mathf.RoundToInt ((gridSizeX - 1) * percentX);
		int y = Mathf.RoundToInt ((gridSizeY - 1) * percentY);

		// On retourne le node placé sur la position
		return grid [x, y];
	}
}