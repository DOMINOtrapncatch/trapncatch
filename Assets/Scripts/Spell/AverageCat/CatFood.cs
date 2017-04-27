using UnityEngine;
using System.Collections;
using System;

public class CatFood : Spell {

    public GameObject foodParticle;
    int restraint = 10; //secondes
    RigidbodyConstraints rgb;
    //manaCost = 20
    

    public override void Activate()
    {
        //get the other cat.position and freeze it
        //check with timer
        StartCoroutine("Freeze");

    }

    IEnumerator Freeze()
    {
        GameObject particleInit = (GameObject)Instantiate(foodParticle, cat.transform.position, Quaternion.identity);
        ParticleSystem food = particleInit.GetComponent<ParticleSystem>();
        food.Play();

        if (cat.nearEnemy.Count > 0)
        {
            rgb = cat.nearEnemy[0].GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        }

        for(int i = 0; i <= restraint; ++i)
        {
            print(i);
            yield return new WaitForSeconds(1.0f);
        }
        rgb = cat.nearEnemy[0].GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        //end particle (noo)
        food.Stop();
        Destroy(particleInit, 0.1f);
    }
}
