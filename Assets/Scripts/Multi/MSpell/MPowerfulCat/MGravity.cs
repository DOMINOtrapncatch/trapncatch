using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class MGravity : MSpell
{
	[Header("Spell data")]
	public GameObject gravityParticle;
	List<GameObject> nearEnemySave;
	List<float> speedEnemy;
	float restraint = 10; //secondes

    public override void Activate()
    {
        if (!isLocalPlayer)
            return;
        StartCoroutine("gravity");
    }

    IEnumerator gravity()
    {
		Dictionary<MCharacter, float> nearEnemySave = new Dictionary<MCharacter, float>();

		// Start particle effect
		GameObject particleInit = (GameObject)Instantiate(gravityParticle, cat.transform.position, gravityParticle.transform.rotation);

		if (cat.aroundEnemy.Count > 0)
		{
			//freez
			foreach (GameObject enemy in cat.aroundEnemy)
			{
				// Get character
				MCharacter chara = enemy.GetComponent<MCharacter>();

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
		foreach(KeyValuePair<MCharacter, float> enemy in nearEnemySave)
			enemy.Key.Speed = enemy.Value;

		// End particle effect
		Destroy(particleInit, 0.1f);
    }
}
