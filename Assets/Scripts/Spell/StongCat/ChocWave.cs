using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ChocWave : Spell
{
    [Header("Spell data")]
    public ParticleSystem particle;
    [Range(0, 100)]
    public int lifeDamage = 30;

    public override void Activate()
    {
        if (!particle.isPlaying)
            particle.Play();

        foreach (GameObject enemy in cat.aroundEnemy)
        {
            Character enemyAll = enemy.GetComponentInParent<Character>();
            float lifeEnemy = enemyAll.Life - lifeDamage;
            enemyAll.Life = lifeEnemy;
        }

        particle.Stop();

    }
}
