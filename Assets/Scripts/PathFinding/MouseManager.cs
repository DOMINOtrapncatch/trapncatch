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
		if (enemySpawnCount < enemySpawnLimit && enemies.Count < enemyCap)
		{
			if(enemySpawnRateCount == 0)
			{
				GameObject mouseProfile = enemiesProfiles[Random.Range(0, enemiesProfiles.Count - 1)];
				Mouse newEnemy = mouseProfile.GetComponent<Mouse>();
				newEnemy.targets.Shuffle();
				newEnemy.transform.position = newEnemy.targets[0].transform.position;
				newEnemy.Speed += Random.Range(-.5f, .5f);

				GameObject newEnemyObject = (GameObject)Instantiate(mouseProfile, newEnemy.transform.position, Quaternion.identity);
				enemies.Add(newEnemyObject);
				newEnemyObject.SetActive(true);

				enemySpawnCount++;
				enemySpawnRateCount++;
			}
			else
			{
				enemySpawnRateCount = (enemySpawnRateCount + 1) % enemySpawnRate;
			}
		}
	}

	public void Remove(GameObject enemy)
	{
		enemies.Remove(enemy);
		enemyKillCount++;
	}
}