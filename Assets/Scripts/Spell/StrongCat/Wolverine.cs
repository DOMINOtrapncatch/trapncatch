using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Wolverine : Spell
{
    [Header("Spell data")]
    public ParticleSystem particle;
    [Range(0,100)]
    public float stopSpeed = 0;

    public override void Activate()
    {
        StartCoroutine("wolverine");
    }

    IEnumerator wolverine()
    {
        if (!particle.isPlaying)
            particle.Play();

        float lifeStart = cat.Life;
        stopSpeed = cat.Speed;
        cat.Speed = 0;

        for (int i = 0; i < (lifeStart / 2); i++)
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

        cat.Speed = stopSpeed;
    }
}
