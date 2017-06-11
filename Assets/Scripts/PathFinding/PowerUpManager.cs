using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PowerUpManager : MonoBehaviour {

	public List<Transform> powerUpSpawnPoints = new List<Transform>();
	public List<GameObject> powerUpProfiles = new List<GameObject>();

	GameObject[] placedPowerUps;

	void Start()
	{
		placedPowerUps = new GameObject[powerUpSpawnPoints.Count];
	}

	void Update ()
	{
		if(Random.Range(0, 20) == 0)
		{
			int randomPlace = Random.Range(0, powerUpSpawnPoints.Count - 1);

			if(placedPowerUps[randomPlace] == null)
			{
				//placedPowerUps = powerUpProfiles[]
			}
		}
	}
}
