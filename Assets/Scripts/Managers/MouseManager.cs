using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MouseManager : MonoBehaviour {
    
	public int mouseCap = 1;
	public int mouseSpawnLimit = 1;
	public int mouseSpawnRate = 20;

	List<GameObject> mouseProfiles = new List<GameObject>();
	List<Transform> mouseTargets = new List<Transform>();

	List<GameObject> mouses = new List<GameObject>();
	int mouseSpawnCount = 0;

	int mouseSpawnRateCount = 0;

	void Start()
	{
        foreach (Mouse mouseProfile in transform.Find("Mouses").GetComponentsInChildren<Mouse>())
        {
            mouseProfile.gameObject.SetActive(false);
            mouseProfiles.Add(mouseProfile.gameObject);
        }

        foreach (Transform mouseTarget in transform.Find("Targets").GetComponentsInChildren<Transform>())
            mouseTargets.Add(mouseTarget);
	}

	void Update()
	{
		// Spawn conditions based on public variables
		if (mouseSpawnCount < mouseSpawnLimit && mouses.Count < mouseCap)
		{
			// Spawn rate handling
			if(mouseSpawnRateCount == 0)
			{
                int randomPositionID = Random.Range(0, mouseTargets.Count);
                List<Transform> randomTargets = mouseTargets.Shuffle();
                Transform randomPosition = randomTargets.ElementAt(randomPositionID);

                // Prevent from nearby spawn
                bool hasFoundMouseAround = false;
                foreach (Collider col in Physics.OverlapSphere(randomPosition.position, 5.0f))
                {
					if (col.tag == "Character")
					{
						hasFoundMouseAround = true;
						break;
					}
                }

                if(!hasFoundMouseAround)
                {
					// Get random mouse profile
					GameObject mouseProfile = mouseProfiles[Random.Range(0, mouseProfiles.Count)];

					// Get corresponding Mouse class
					Mouse newMouse = mouseProfile.GetComponent<Mouse>();

					// Create the next object, instanciate it into the world, add it to the mouses list and set it active
					GameObject newMouseObject = (GameObject)Instantiate(mouseProfile, randomPosition.position, Quaternion.identity);
					mouses.Add(newMouseObject);
					// Edit some variables to add randomness then activate then start pathfind if necessary
					newMouseObject.GetComponent<Mouse>().Speed += Random.Range(-.5f, .5f);
					newMouseObject.GetComponent<Mouse>().InitTargets(randomPositionID, randomTargets);
					newMouseObject.SetActive(true);
                    newMouseObject.GetComponent<Mouse>().StartUpdating();

					// Increment some counters
					mouseSpawnCount++;
                }
			}

			mouseSpawnRateCount = (mouseSpawnRateCount + 1) % mouseSpawnRate;
		}
	}

	public void Remove(GameObject mouse)
	{
		// Removes an mouse from the mouses list
		mouses.Remove(mouse);
	}
}