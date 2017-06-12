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

        List<float> speedEnemy = new List<float>();
        List<GameObject> AroundEnemy = cat.aroundEnemy;

        foreach(GameObject enemy in AroundEnemy)
        {
            Character CatEnemy = (Character) enemy.GetComponent(typeof(Character));
            speedEnemy.Add(CatEnemy.Speed);
            CatEnemy.Speed = 0;
        }

        yield return new WaitForSeconds(4.0f);

        particle.Stop();

        for(int i = 0; i< AroundEnemy.Count; i++)
        {
            AroundEnemy[i].GetComponent<Character>().Speed = speedEnemy[i];
        }
        speedEnemy.Clear();
        AroundEnemy.Clear();
    }
}
