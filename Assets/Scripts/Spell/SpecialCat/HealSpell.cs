using UnityEngine;
using System.Collections;
using System;

public class HealSpell : Spell {

	[Header("Spell Data")]

	public ParticleSystem particle;
	[Range(0, 100)]
	public int lifeAmount = 20;

	public override void Activate()
	{
		StartCoroutine("LifeUp");
	}

	private IEnumerator LifeUp()
	{
		if (!particle.isPlaying)
			particle.Play();
		
		for (int i = 0; i < lifeAmount; i++)
		{
			if (cat.Life + 1 > cat.maxLife)
				break;
			
			cat.Life += 1;
			yield return new WaitForSeconds(0.2f);
		}

		particle.Stop();
	}
}
