using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class Gravity : Spell
{
    [Header("Spell data")]
    public ParticleSystem particle;

    public override void Activate()
    {
        StartCoroutine("gravity");
    }

    IEnumerator gravity()
    {
        if (!particle.isPlaying)
            particle.Play();

        foreach(GameObject enemy in cat.aroundEnemy)
        {
            Character CatEnemy = (Character) enemy.GetComponent(typeof(Character));
            CatEnemy.Speed = 0;
        }

        yield return new WaitForSeconds(3.0f);

        particle.Stop();
    }
}
