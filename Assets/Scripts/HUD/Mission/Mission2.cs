using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Mission2 : MissionBase
{

    override public void CheckTooltips()
    {
        switch (currentTooltip)
        {
            case 1:
                if (CheckTooltip1())
                {
                    SaveManager.SaveMission(2);
                    AutoFade.LoadLevel(9, .3f, .3f, Color.black);
                }
                break;
        }
    }

    /*
	 * Objectif: tuer un nombre de souris fixées
	 */
    bool CheckTooltip1()
    {
        return myHUD.player.enemyKillCount >= 4;
    }

}
