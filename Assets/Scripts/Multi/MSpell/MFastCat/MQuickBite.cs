using UnityEngine;
using System.Collections;

public class MQuickBite : MSpell
{
    [Header("QuickBite settings")]
    [Range(0, 10)]
    public float dashDuration = 1F;
    [Range(1, 1000)]
    public float dashPropulsion = 200F;
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

        Rigidbody rb = cat.GetComponent<Rigidbody>();

        rb.AddForce(transform.forward * dashPropulsion, ForceMode.Impulse);
        yield return new WaitForSeconds(dashDuration);

        cat.attacks[0].Activate();
        particle.Stop();

        yield return null;
    }
}
