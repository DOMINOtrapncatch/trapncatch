using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class Gravity : Spell
{
	[Header("Spell data")]
	public GameObject gravityParticle;
	List<GameObject> nearEnemySave;
	List<float> speedEnemy;
	float restraint = 10; //secondes

    public override void Activate()
    {
        StartCoroutine("gravity");
    }

    IEnumerator gravity()
    {
		Dictionary<Character, float> nearEnemySave = new Dictionary<Character, float>();

		// Start particle effect
		GameObject particleInit = (GameObject)Instantiate(gravityParticle, cat.transform.position, gravityParticle.transform.rotation);

		if (cat.aroundEnemy.Count > 0)
		{
			//freez
			foreach (GameObject enemy in cat.aroundEnemy)
			{
				// Get character
				Character chara = enemy.GetComponent<Character>();

				if (enemy != null && !nearEnemySave.ContainsKey(chara))
				{
					// Save its data
					nearEnemySave.Add(chara, chara.Speed);

					// Set its speed
					chara.Speed = 0;
				}
			}
		}

		yield return new WaitForSeconds(restraint);

		//unfreez
		foreach(KeyValuePair<Character, float> enemy in nearEnemySave)
			enemy.Key.Speed = enemy.Value;

		// End particle effect
		Destroy(particleInit, 0.1f);
    }
}
