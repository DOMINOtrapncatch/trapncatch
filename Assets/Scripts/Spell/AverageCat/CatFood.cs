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
        nearEnemySave = cat.aroundEnemy;
        if (nearEnemySave.Count > 0)
        {

            GameObject particleInit = (GameObject)Instantiate(foodParticle, cat.transform.position, Quaternion.identity);
            ParticleSystem food = particleInit.GetComponent<ParticleSystem>();
            food.Play();

            //freez
            foreach (GameObject enemy in nearEnemySave)
            {
                Character chara = enemy.GetComponent<Character>();
                speedEnemy.Add(chara.Speed);
                chara.Speed = 0;
            }

            yield return new WaitForSeconds(restraint);

            //unfreez
            for (int i = 0; i < nearEnemySave.Count; ++i)
            {
                nearEnemySave[i].GetComponent<Character>().Speed = speedEnemy[i];
            }
            //end particle (noo)
            nearEnemySave.Clear();
            speedEnemy.Clear();
            food.Stop();
            Destroy(particleInit, 0.1f);
        }
    }
}
