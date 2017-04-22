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

		cat.Life += lifeAmount;
		if (cat.Life > cat.MaxLife)
			cat.Life = cat.MaxLife;
	}
}
