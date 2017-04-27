using UnityEngine;
using System.Collections;

public class QuickBite : Spell
{
    [Header("QuickBite settings")]
    [Range(0, 100)]
    public float dashDuration = 3F;
    [Range(0, 1500)]
    public float dashPropulsion = 250F;
    public ParticleSystem particle;

    public override void Activate()
    {
        StartCoroutine(Effect());
    }

    IEnumerator Effect()
    {

        if (!particle.isPlaying)
            particle.Play();

        Rigidbody rb = gameObject.GetComponent<Rigidbody>();
        float duration = dashDuration;

        while (duration > 0)
        {
            rb.AddForce(transform.forward * dashPropulsion);
            duration -= .2F;

            yield return new WaitForSeconds(.01F);
        }
        
        cat.spells[0].Activate();

        particle.Stop();

        yield return null;
    }
}
