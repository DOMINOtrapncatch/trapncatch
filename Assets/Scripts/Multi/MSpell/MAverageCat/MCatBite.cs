using UnityEngine;
using System.Collections;
using System;

public class MCatBite : MSpell {

    //AVERAGE CAT

    public int damage = 20;
    // manaCost = 5
    public GameObject particle;
    private bool isDead;
    

    public override void Activate()
    {
        if (!isLocalPlayer)
            return;

        StartCoroutine(Bite());
    }

    IEnumerator Bite()
    {
        GameObject particleInit = (GameObject)Instantiate(particle, cat.transform.position, Quaternion.identity);

        //basic_spell more powerfull
        foreach (GameObject other in cat.aroundEnemy)
        {
            MCat catEnemy = other.GetComponent<MCat>();
            MMouse miceEnemy = other.GetComponent<MMouse>();
            if (catEnemy != null && catEnemy.Life > 0)
            {
                isDead = catEnemy.Damage(20);
                if (isDead)
                    catEnemy.Destroy();
            }
            else if (miceEnemy != null && miceEnemy.Life > 0)
            {
                isDead = miceEnemy.Damage(20);
                if (isDead)
                    miceEnemy.Destroy();
            }
        }

        //update current cat mana
        cat.Mana -= manaCost;

        yield return new WaitForSeconds(0.1f);
        //end particle (noo)
        Destroy(particleInit, 1.0f);
    }

  

    
}
