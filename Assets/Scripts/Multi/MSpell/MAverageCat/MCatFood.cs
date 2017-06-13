using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class MCatFood : MSpell {

	public GameObject foodParticle;
	float restraint = 10; //secondes
	//manaCost = 20


	public override void Activate()
	{
        if (!isLocalPlayer)
            return;
		//get the other cat.position and freeze it
		//check with timer
		StartCoroutine("Freeze");
	}

	IEnumerator Freeze()
	{
		Dictionary<MCharacter, float> nearEnemySave = new Dictionary<MCharacter, float>();

		// Start particle effect
		GameObject particleInit = (GameObject)Instantiate(foodParticle, cat.transform.position + Vector3.up * .25f, foodParticle.transform.rotation);

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
