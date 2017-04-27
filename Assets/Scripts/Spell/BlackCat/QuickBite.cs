using UnityEngine;
using System.Collections;

public class QuickBite : Spell
{
    [Header("QuickBite settings")]
    [Range(0, 100)]
    public float dashDuration = 3F;
    public Vector3 direction = new Vector3(0, 0, .1F);
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
            rb.AddForce(transform.forward + direction);
            duration -= .1F;

            yield return new WaitForSeconds(.1F);
        }

        cat.spells[0].Activate();

        particle.Stop();

        yield return null;
    }
}
