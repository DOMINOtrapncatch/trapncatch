using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;

public class Mission4 : MissionBase {

    public string tooltip = "Survivez";

    public List<Cat> players;
    public List<Mouse> enemies;

    private bool playersAllDead = false;

    //mouse manager 
    //disons moins de 10 souris meurtrieres
    public override void CheckTooltips()
    {
        throw new NotImplementedException();
    }
    
}
