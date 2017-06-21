using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class PowerUpManager : MonoBehaviour
{
    public List<Transform> powerUpSpawnPoints = new List<Transform>();
    public List<GameObject> powerUpProfiles = new List<GameObject>();
    public List<GameObject> powerUpDeathProfiles = new List<GameObject>();

    public int randomSpawnValue = 500;

    Dictionary<GameObject, Transform> placedPowerUps = new Dictionary<GameObject, Transform>();
    Dictionary<GameObject, int> placedPowerUpsProfileID = new Dictionary<GameObject, int>();

    void Update()
    {
        // Check for setup existence
        if (powerUpProfiles.Count > 0 && powerUpSpawnPoints.Count > 0)
        {
            if (placedPowerUps.Count <= powerUpProfiles.Count && Random.Range(0, randomSpawnValue) == 0)
            {
                int randomPlace = Random.Range(0, powerUpSpawnPoints.Count);

                // If place isn't alwready taken
                if (!placedPowerUps.Values.Contains(powerUpSpawnPoints[randomPlace]))
                {
                    int randomProfile = Random.Range(0, powerUpProfiles.Count);

                    // Instanciate the random power up
                    GameObject newPowerUpObject = (GameObject)Instantiate(powerUpProfiles[randomProfile], powerUpSpawnPoints[randomPlace].transform.position, powerUpProfiles[randomProfile].transform.rotation);
                    placedPowerUps.Add(newPowerUpObject, powerUpSpawnPoints[randomPlace]);
					placedPowerUpsProfileID.Add(newPowerUpObject, randomProfile);
                    newPowerUpObject.SetActive(true);
                }
            }
        }
    }

    public void Remove(GameObject powerUp)
    {
		// Play death animation
		int powerUpProfileID = placedPowerUpsProfileID[powerUp];
		GameObject newPowerUpDeathObject = (GameObject)Instantiate(powerUpDeathProfiles[powerUpProfileID], placedPowerUps[powerUp].transform.position, powerUpDeathProfiles[powerUpProfileID].transform.rotation);

        // Removes a power up from the list of placed power ups
        placedPowerUps.Remove(powerUp);
		placedPowerUpsProfileID.Remove(powerUp);
    }
}
