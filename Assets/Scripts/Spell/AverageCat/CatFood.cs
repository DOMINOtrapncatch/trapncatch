using UnityEngine;
using System.Collections;
using System;

public class CatFood : Spell {

    int restraint = 45; //secondes
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
        if(cat.nearEnemy.Count > 0)
        {
            rgb = cat.nearEnemy[0].GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        }

        for(int i = 0; i < restraint; ++i)
        {

            yield return new WaitForSeconds(1.0f);
        }

        rgb = cat.nearEnemy[0].GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;

    }
}
