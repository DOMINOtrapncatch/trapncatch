using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Wolverine : Spell
{
    [Header("Spell data")]
    public ParticleSystem particle;

    public override void Activate()
    {
        StartCoroutine("wolverine");
    }

    IEnumerator wolverine()
    {
        if (!particle.isPlaying)
            particle.Play();

        for (int i = 0; i < (cat.Life / 2); i++)
        {
            if (cat.Life > cat.MaxLife)
            {
                cat.Life = cat.MaxLife;
                break;
            }

            cat.Life += 1;

            yield return new WaitForSeconds(0.8f);
        }

        particle.Stop();
    }
}
