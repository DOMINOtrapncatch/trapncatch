using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Gravity : Spell
{
    [Header("Spell data")]
    public ParticleSystem particle;

    public override void Activate()
    {
        StartCoroutine("gravity");
    }

    IEnumerator gravity()
    {
        if (!particle.isPlaying)
            particle.Play();

        yield return new WaitForSeconds(5.0f);

        particle.Stop();
    }
}
