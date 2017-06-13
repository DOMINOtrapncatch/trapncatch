using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MWolverine : MSpell
{
    [Header("Spell data")]

    public ParticleSystem particle;
	[Range(0, 100)]
	public int lifeAmount = 20;

    public override void Activate()
    {
        if (!isLocalPlayer)
            return;
        StartCoroutine("wolverine");
	}

	public override bool CanUse()
	{
		if (cat.life == cat.maxLife)
			return false;
		else
			return base.CanUse();
	}

    IEnumerator wolverine()
    {
		if (!particle.isPlaying)
			particle.Play();

		for (int i = 0; i < lifeAmount && cat.Life < cat.maxLife; i++)
		{
			cat.Heal(1);
			yield return new WaitForSeconds(0.2f);
		}

		particle.Stop();
    }
}
