using UnityEngine;
using System.Collections;
using System;

public class NightVision : Spell {
    
    //askip etat passif
    public GameObject vision;
    public int hauteur = 8;
    public float delay = 5.0f;
    public Light mainLight;

    private Light spotlight;

    void Start()
    {
        spotlight = vision.GetComponent<Light>();
        //mainLight = GetComponent<Light>();
    }

    

    public override void Activate()
    {

        StartCoroutine("LightsOff");

        if (mainLight.enabled)
            return;

        SpotLight();
    }

    public void SpotLight()
    {
        float catX = cat.transform.position.x;
        float catY = cat.transform.position.y + hauteur;
        float catZ = cat.transform.position.z;
        Light spotlight = Instantiate(vision, new Vector3(catX, catY, catZ), Quaternion.identity) as Light;

        //put it inside cat
        spotlight.transform.parent = cat.transform;
    }

    IEnumerator LightsOff()
    {
        mainLight.enabled = false;

        for (float i = 0; i < delay; ++i)
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
