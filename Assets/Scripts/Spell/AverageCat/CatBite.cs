using UnityEngine;
using System.Collections;
using System;

public class CatBite : Spell {

    //AVERAGE CAT

    public int damage = 20;
    public float manaCost = 5;
    public GameObject particle;
    

    public override void Activate()
    {
        particle = (GameObject)Instantiate(particle, cat.transform.position, Quaternion.identity);

        //basic_spell more powerfull
        foreach(GameObject other in cat.aroundEnemy)
        {
            Cat catEnemy = other.GetComponent<Cat>();
            Mouse miceEnemy = other.GetComponent<Mouse>();
            if(catEnemy != null && catEnemy.Life > 0)
            {
                catEnemy.Life -= damage;
            }
            else if(miceEnemy != null && miceEnemy.Life > 0)
            {
                miceEnemy.Life -= damage;
            }
        }

        //update current cat mana
        cat.Mana -= manaCost;

        //end particle
        Destroy(particle, 2.0f);
    }

  

    
}
