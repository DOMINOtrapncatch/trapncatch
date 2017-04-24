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
		particle.Stop();
		particle.transform.position = cat.transform.position;
		particle.Play();

		StartCoroutine("LifeUp");
	}

	private IEnumerator LifeUp()
	{
		for (int i = 0; i < lifeAmount; i++)
		{
			cat.Life += 1;
			yield return new WaitForSeconds(0.2f);
		}
	}
}
