using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ChocWave : Spell
{
    [Header("Spell data")]
    public ParticleSystem particle;

    public override void Activate()
    {
       
    }

    IEnumerator chocwave()
    {
        if (!particle.isPlaying)
            particle.Play();

        return null;

        particle.Stop();
    }
}
