using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;

public class Mission4 : MissionBase {

    //public string tooltip = "Survivez";
    public MouseManager mafia;

    //private bool playersAllDead = false;

    //mouse manager 
    //disons moins de 10 souris meurtrieres
    public override void CheckTooltips()
    {
        if(Checktip1())
        {
            ChooseMission.Success();
            SaveManager.Set("mission4", "1");
        }
    }

    private bool Checktip1()
    {
        return myHUD.player.enemyKillCount == mafia.enemyCap;
    }
    
}
