using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Grid : MonoBehaviour {

	public bool displayGridGizmos;

	public LayerMask unwalkableMask;
	public Vector3 gridWorldSize;
	public float nodeRadius;

	public TerrainType[] walkableRegions;
	public int obstaclePenalty = 20;
	public int blurSize = 3;
	LayerMask walkableMask;
	Dictionary<int, int> walkableRegionsDictionnary = new Dictionary<int, int>();

	Node[,] grid;

	float nodeDiameter;
	int gridSizeX, gridSizeY;

	void Awake()
	{
		// Initialisation des variables
		nodeDiameter = nodeRadius * 2;
		gridSizeX = Mathf.RoundToInt (gridWorldSize.x / nodeDiameter);
		gridSizeY = Mathf.RoundToInt (gridWorldSize.y / nodeDiameter);

		foreach(TerrainType region in walkableRegions)
		{
			walkableMask.value |= region.terrainMask.value; // L'operation "|=" permet de "combiner" les differents mask car unity les store en binaire (Layer 2 = 10, Layer 3 = 100 ==> Layer2 | Layer3 = 110)
			walkableRegionsDictionnary.Add((int)Mathf.Log(region.terrainMask.value, 2), region.terrainPenalty); // Le log est la pour recup le num du layer: log(pow(2,3), 2) = 3 ==> log(100, 2) = 3
		}

		// Creation de la grille
		CreateGrid ();
	}

	public int MaxSize { get { return gridSizeX * gridSizeY; } }

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

				// Calculating movement penalty with raycast method
				int movementPenalty = 0;

				Ray ray = new Ray(worldPoint + Vector3.up * 50, Vector3.down); // Ray starting up in the sky and going straight down
				RaycastHit hit;

				if(Physics.Raycast(ray, out hit, 100, walkableMask))
					walkableRegionsDictionnary.TryGetValue(hit.collider.gameObject.layer, out movementPenalty);

				if (!walkable)
					movementPenalty += obstaclePenalty;

				// On ajoute le node à la grille
				grid [x, y] = new Node (walkable, worldPoint, x, y, movementPenalty);
			}
		}

		BlurPenaltyMap();
	}

	/*
	 * Permet de "flouter" le masque de penalite pour avoir un mvt plus naturel (le seeker ne se colle plus aux bordures)
	 */
	void BlurPenaltyMap()
	{
		// NOTE: La methode de bluring utilisee est une methode opti (d'ou le code un peu chelou...)

		int kernelSize = blurSize * 2 + 1;
		int kernelExtents = (kernelSize - 1) / 2;

		int[,] penaltiesHorizontalPass = new int[gridSizeX, gridSizeY];
		int[,] penaltiesVerticalPass = new int[gridSizeX, gridSizeY];

		// Calculating horizontal bluring
		for (int y = 0; y < gridSizeY; y++)
		{
			for (int x = -kernelExtents; x <= kernelExtents; x++)
			{
				int sampleX = Mathf.Clamp(x, 0, kernelExtents);
				penaltiesHorizontalPass[0, y] += grid[sampleX, y].movementPenalty;
			}

			for (int x = 1; x < gridSizeX; x++)
			{
				int removeIndex = Mathf.Clamp(x - kernelExtents - 1, 0, gridSizeX);
				int addIndex = Mathf.Clamp(x + kernelExtents, 0, gridSizeX - 1);

				penaltiesHorizontalPass[x, y] = penaltiesHorizontalPass[x - 1, y] - grid[removeIndex, y].movementPenalty + grid[addIndex, y].movementPenalty;
			}
		}

		// Calculating vertical bluring
		for (int x = 0; x < gridSizeX; x++)
		{
			for (int y = -kernelExtents; y <= kernelExtents; y++)
			{
				int sampleY = Mathf.Clamp(y, 0, kernelExtents);
				penaltiesVerticalPass[x, 0] += penaltiesHorizontalPass[x, sampleY];
			}

			// Handle first row
			int blurredPenalty = Mathf.RoundToInt((float)penaltiesVerticalPass[x, 0] / (kernelSize * kernelSize));
			grid[x, 0].movementPenalty = blurredPenalty;

			for (int y = 1; y < gridSizeY; y++)
			{
				int removeIndex = Mathf.Clamp(y - kernelExtents - 1, 0, gridSizeY);
				int addIndex = Mathf.Clamp(y + kernelExtents, 0, gridSizeY - 1);

				penaltiesVerticalPass[x, y] = penaltiesVerticalPass[x, y - 1] - penaltiesHorizontalPass[x, removeIndex] + penaltiesHorizontalPass[x, addIndex];

				// Calculates and set the final penalty to the grid nodes
				blurredPenalty = Mathf.RoundToInt((float)penaltiesVerticalPass[x, y] / (kernelSize * kernelSize));
				grid[x, y].movementPenalty = blurredPenalty;

				// DEBUG - Drawing gizoms
				if (blurredPenalty > penaltyMax)
					penaltyMax = blurredPenalty;
				if (blurredPenalty < penaltyMin)
					penaltyMin = blurredPenalty;
			}
		}
	}

	/*
	 * Obtenir une liste de tous les voisins d'un node
	 */
	public List<Node> GetNeighbors(Node node)
	{
		List<Node> neighbors = new List<Node> ();

		for(int x = -1; x <= 1; x++)
		{
			for(int y = -1; y <= 1; y++)
			{
				if (x == 0 && y == 0)
					continue;
				
				int checkX = node.gridX + x;
				int checkY = node.gridY + y;
				if (0 <= checkX && checkX < gridSizeX && 0 <= checkY && checkY < gridSizeY)
					neighbors.Add (grid [checkX, checkY]);
			}
		}

		return neighbors;
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

	/*
	 * DEBUG - Affichage de la grille de nodes et de leur etat
	 */
	int penaltyMin = int.MaxValue;
	int penaltyMax = int.MinValue;
	void OnDrawGizmos()
	{
		Gizmos.DrawWireCube (transform.position, new Vector3(gridWorldSize.x, 1, gridWorldSize.y));

		if(grid != null && displayGridGizmos)
		{
			foreach(Node n in grid)
			{
				Gizmos.color = Color.Lerp(Color.white, Color.black, Mathf.InverseLerp(penaltyMin, penaltyMax, n.movementPenalty));
				Gizmos.color = n.walkable ? Gizmos.color : Color.red;
				Gizmos.DrawCube (n.worldPosition, Vector3.one * (nodeDiameter));
			}
		}
	}

	[System.Serializable]
	public class TerrainType {
		public LayerMask terrainMask;
		public int terrainPenalty;
	}
}