using System.Collections.Generic;
using UnityEngine;

public class MouseManager : MonoBehaviour {
	
	public List<GameObject> enemiesProfiles = new List<GameObject>();
	public int enemyCap = 1;
	public int enemySpawnLimit = 1;
	public int enemySpawnRate = 20;

	List<GameObject> enemies = new List<GameObject>();
	int enemyKillCount = 0;
	int enemySpawnCount = 0;

	int enemySpawnRateCount = 0;

	void Start()
	{
		foreach (GameObject enemyProfile in enemiesProfiles)
			enemyProfile.SetActive(false);
	}

	void Update()
	{
		// Spawn conditions based on public variables
		if (enemySpawnCount < enemySpawnLimit && enemies.Count < enemyCap)
		{
			// Spawn rate handling
			if(enemySpawnRateCount == 0)
			{
                // Get random mouse profile
                int rand = Random.Range(0, enemiesProfiles.Count - 1);
				GameObject mouseProfile = enemiesProfiles[rand];

				// Get corresponding Mouse class and edit some variables to add randomness
				Mouse newEnemy = mouseProfile.GetComponent<Mouse>();
				if(newEnemy.mouseIA != MouseIA.SUICIDE)
					newEnemy.targets.Shuffle();
				newEnemy.transform.position = newEnemy.targets[0].transform.position;
				newEnemy.Speed += Random.Range(-.5f, .5f);

                // Create the nex object, instanciate it into the world, add it to the enemies list and set it active
                if ((newEnemy.mouseIA == MouseIA.SUICIDE && newEnemy.targets[1] != null) || newEnemy.mouseIA != MouseIA.SUICIDE)
                {
                    GameObject newEnemyObject = (GameObject)Instantiate(mouseProfile, newEnemy.transform.position, Quaternion.identity);
                    enemies.Add(newEnemyObject);
                    newEnemyObject.SetActive(true);

                    // increment sme counters
                    enemySpawnCount++;
                    enemySpawnRateCount++;
                }
			}
			else
			{
				enemySpawnRateCount = (enemySpawnRateCount + 1) % enemySpawnRate;
			}
		}
	}

	public void Remove(GameObject enemy)
	{
		// Removes an enemy from the enemies list
		enemies.Remove(enemy);
		enemyKillCount++;
	}
}