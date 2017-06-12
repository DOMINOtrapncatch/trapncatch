using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class CatFood : Spell {

    public GameObject foodParticle;
    float restraint = 10; //secondes
    //manaCost = 20
    
    
    public override void Activate()
    {
        //get the other cat.position and freeze it
        //check with timer
       StartCoroutine("Freeze");
    }

    IEnumerator Freeze()
    {
		Dictionary<Character, float> nearEnemySave = new Dictionary<Character, float>();

		// Start particle effect
		GameObject particleInit = (GameObject)Instantiate(foodParticle, cat.transform.position + Vector3.up * .25f, foodParticle.transform.rotation);

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
