using UnityEngine;
using System.Collections;

public class QuickBite : Spell
{
    [Header("QuickBite settings")]
    [Range(1, 100)]
    public float dashDuration = 1F;
    [Range(1, 1000)]
    public float dashPropulsion = 2F;
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
        /*float duration = dashDuration;

        while (duration > 0)
        {
            rb.AddForce(transform.forward * dashPropulsion);
            duration -= .1F;

            yield return new WaitForSeconds(.02F);
        }*/

        float startTime = Time.fixedTime;

        rb.AddForce(transform.forward * dashPropulsion);
        while (Time.fixedTime - startTime < 2.5f)
        {
            yield return new WaitForSeconds(0.2f);
        }

        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        cat.spells[0].Activate();

        particle.Stop();

        yield return null;
    }
}
