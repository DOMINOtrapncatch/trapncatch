using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class CatFood : Spell {

    public GameObject foodParticle;
    List<GameObject> nearEnemySave;
    List<float> speedEnemy;
    float restraint = 10; //secondes
    //manaCost = 20
    
    
    public override void Activate()
    {
        //get the other cat.position and freeze it
        //check with timer

       speedEnemy = new List<float>();
       StartCoroutine("Freeze");
        
    }

    IEnumerator Freeze()
    {
        nearEnemySave = cat.nearEnemy;
         if (nearEnemySave.Count > 0)
        {

            GameObject particleInit = (GameObject)Instantiate(foodParticle, cat.transform.position, Quaternion.identity);
            ParticleSystem food = particleInit.GetComponent<ParticleSystem>();
            food.Play();
            //freez
            foreach (GameObject enemy in nearEnemySave)
            {
                if (enemy != null)
                {
                    Character chara = enemy.GetComponent<Character>();
                    speedEnemy.Add(chara.Speed);
                    chara.Speed = 0;
                }
            }
            Debug.Log("near saw 1 : " + nearEnemySave.Count);
            Debug.Log("saved  : " + speedEnemy.Count);
            yield return new WaitForSeconds(restraint);

            Debug.Log("near saw 2 : " + nearEnemySave.Count);
            Debug.Log("saved 2 : " + speedEnemy.Count);
            //unfreez
            for(int i = 0; i < nearEnemySave.Count; ++i)
            {
                Character victim = nearEnemySave[i].GetComponent<Character>();
                victim.Speed = speedEnemy[i];
                nearEnemySave.RemoveAt(i);
                speedEnemy.RemoveAt(i);

            }
            //end particle (noo)
            food.Stop();
            Destroy(particleInit, 0.1f);

            
        }
    }
}
