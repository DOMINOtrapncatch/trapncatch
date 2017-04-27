using UnityEngine;
using System.Collections;
using System;

public class NightVision : Spell {
    
    //askip etat passif
    public Light vision;
    public int hauteur = 8;
    private Light mainLight;

    void Start()
    {
        mainLight = GetComponent<Light>();
    }

    void Update()
    {
        if (!mainLight.enabled)
            Activate();
        
    }

    public override void Activate()
    {
        float catX = cat.transform.position.x;
        float catY = cat.transform.position.y + hauteur;
        float catZ = cat.transform.position.z;
        Light spotlight = Instantiate(vision, new Vector3(catX, catY, catZ), Quaternion.identity) as Light;

        //put it inside cat
        spotlight.transform.parent = cat.transform;
    }
}
