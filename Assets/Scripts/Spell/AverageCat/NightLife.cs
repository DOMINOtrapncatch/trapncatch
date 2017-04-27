using UnityEngine;
using System.Collections;

public class NightLife : Spell {

    //Need Event System for when light are turned off or condition with the light gameobject
    public float manaCost = 30;
    public float delay = 5.0f;//secondes
    private Light mainLight;

    void Start()
    {
        mainLight = GetComponent<Light>();
    }

    public override void Activate()
    {
        StartCoroutine("LightsOff");
    }

    IEnumerator LightsOff()
    {
        mainLight.enabled = false;

        for(float i = 0; i < delay; ++i)
        {
            ++delay;
            yield return new WaitForSeconds(1.0f);
        }
        //destroy cat's light
        Light spotlight = cat.GetComponent<Light>();
        Destroy(spotlight);

        mainLight.enabled = true;
    }
}
