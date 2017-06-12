using UnityEngine;
using System.Collections;

public class QuickBite : Spell
{
    [Header("QuickBite settings")]
    [Range(1, 100)]
    public float dashDuration = 1F;
    [Range(1, 1000)]
    public float dashPropulsion = 200F;
    public ParticleSystem particle;

    public override void Activate()
    {
        Rigidbody rb = cat.gameObject.GetComponent<Rigidbody>();
        rb.AddForce(cat.transform.forward * dashPropulsion, ForceMode.Impulse);
        //StartCoroutine(Effect(rb));
    }

    IEnumerator Effect(Rigidbody rb)
    {

        if (!particle.isPlaying)
            particle.Play();

        yield return new WaitForSeconds(1f);
        
        #region Comments
        /*float duration = dashDuration;

        while (duration > 0)
        {
            rb.AddForce(transform.forward * dashPropulsion);
            duration -= .1F;

            yield return new WaitForSeconds(.02F);
        }*/

        /*float startTime = Time.fixedTime;

        rb.AddForce(transform.forward * dashPropulsion);

        /*while (Time.fixedTime - startTime < 2.5f)
        {
            yield return new WaitForSeconds(0.2f);
        }*/

        //rb.AddForce(-transform.forward * dashPropulsion);
        //rb.velocity = Vector3.zero;
        //rb.angularVelocity = Vector3.zero;
        #endregion

        cat.spells[0].Activate();

        particle.Stop();

        yield return null;
    }
}
