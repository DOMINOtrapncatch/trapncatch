using UnityEngine;
using System.Collections;

public class MultipleScratch : Spell
{
    [Range(0, 100)]
    public int numberOfAttacks = 10;
    public ParticleSystem particle;

    public override void Activate()
    {
        int i = 10;
        while (i > 0)
        {
            cat.spells[0].Activate();
            --i;
        }
    }

    IEnumerator Effect()
    {
        if (!particle.isPlaying)
            particle.Play();

        int i = numberOfAttacks;
        while (i > 0)
        {
            cat.spells[0].Activate();
            --i;
        }

        particle.Stop();

        yield return null;
    }
}