using UnityEngine;
using System.Collections;

public class MSacredSteroid : MSpell
{
    [Range(0, 100)]
    public float attackIncrease = 25F;
    [Range(0, 100)]
    public float speedDecrease = 25F;
    [Range(0, 100)]
    public float boostDuration = 25F;
    public ParticleSystem particle;

    public override void Activate()
    {
        if (!isLocalPlayer)
            return;

        StartCoroutine(Effect());
    }

    IEnumerator Effect()
    {
        if (!particle.isPlaying)
            particle.Play();

        cat.Attack += attackIncrease;
        cat.Speed -= speedDecrease;

        yield return new WaitForSeconds(boostDuration);

        cat.Attack -= attackIncrease;
        cat.Speed += speedDecrease;

        particle.Stop();
    }
}
