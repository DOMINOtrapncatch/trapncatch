﻿using UnityEngine;
using System.Collections;

public class MultipleScratch : Spell
{
    [Range(0, 100)]
    public int numberOfAttacks = 10;
    public ParticleSystem particle;

    public override void Activate()
    {
        StartCoroutine(Effect());
    }

    IEnumerator Effect()
    {
        if (!particle.isPlaying)
            particle.Play();

        int i = numberOfAttacks;
        while (i > 0)
        {
            cat.attacks[0].Activate();
            --i;
        }

        particle.Stop();

        yield return null;
    }
}