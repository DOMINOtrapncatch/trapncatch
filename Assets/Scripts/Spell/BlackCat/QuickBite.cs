using UnityEngine;
using System.Collections;

public class QuickBite : Spell
{
    public override void Activate()
    {
        /*Rigidbody rb = gameObject.GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * 15000);
        cat.spells[0].Activate();*/
        StartCoroutine(Effect());
    }

    IEnumerator Effect()
    {
        Rigidbody rb = gameObject.GetComponent<Rigidbody>();
        float timeLeft = 250F;

        while (timeLeft > 0)
        {
            rb.AddForce(transform.forward + new Vector3(0, 0, .1F));
            timeLeft -= Time.deltaTime;
        }
        cat.spells[0].Activate();
        yield return null;
    }
}
