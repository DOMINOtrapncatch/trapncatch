using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class MHealSpell : MSpell {

	[Header("Spell Data")]

	public ParticleSystem particle;
	[Range(0, 100)]
	public int lifeAmount = 20;

	public override void Activate()
	{
        if (!isLocalPlayer)
            return;
		StartCoroutine("LifeUp");
	}

	public override bool CanUse()
	{
		if (cat.life == cat.maxLife)
			return false;
		else
			return base.CanUse();
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
