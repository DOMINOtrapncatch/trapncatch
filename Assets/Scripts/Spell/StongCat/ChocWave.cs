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
        StartCoroutine("ChocWaveAction");
    }
    
    IEnumerator ChocWaveAction()
    {
        if (!particle.isPlaying)
            particle.Play();

        foreach (GameObject enemy in cat.aroundEnemy)
        {
            Character enemyAll = (Character)enemy.GetComponent(typeof(Character));
            if (!enemyAll.Damage(lifeDamage))
            {
                enemyAll.Destroy();
                cat.enemyKillCount++;
            }
        }

        yield return new WaitForSeconds(2.0f);

        particle.Stop();
    }
}
