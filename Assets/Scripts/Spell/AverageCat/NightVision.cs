using UnityEngine;
using System.Collections;
using System;

public class NightVision : Spell {

    //Need Event System for when light are turned off or condition with the light gameobject
    Light light;

    void Start()
    {
        manaCost = 0;
        light = GetComponent<Light>();
    }

    public override void Activate()
    {
        if(!light.enabled)
        {
            //timer for the spell
            //visual effect -> ghost or light reduced 50%
            //FIXME
        }
    }
}
