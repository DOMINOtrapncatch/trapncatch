using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class SpeedUp : Spell
{
    [Header("Spell data")]
    public ParticleSystem particle;
    [Range(0, 100)]
    public int SpeedAmount = 20;

    public override void Activate()
    {
        StartCoroutine("speed");
    }

    IEnumerator speed()
    {
        if (!particle.isPlaying)
            particle.Play();

        cat.Speed += SpeedAmount;
        yield return new WaitForSeconds(8.0f);
        cat.Speed -= SpeedAmount;

        particle.Stop();
    }
}