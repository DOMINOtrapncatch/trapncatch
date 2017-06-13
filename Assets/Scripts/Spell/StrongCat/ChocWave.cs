﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ChocWave : Spell
{
    [Header("Spell data")]
	public GameObject particle;
    [Range(0, 100)]
    public int lifeDamage = 30;

    public override void Activate()
    {
        StartCoroutine("ChocWaveAction");
    }
    
    IEnumerator ChocWaveAction()
    {
		GameObject particleInit = (GameObject)Instantiate(particle, cat.transform.position + Vector3.up * .25f, particle.transform.rotation);

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

		Destroy(particleInit);
    }
}
