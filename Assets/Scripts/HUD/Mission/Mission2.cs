using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


public class Mission2 : MissionBase
{
    // Tooltips variables
    int tooltip1;

    override public void CheckTooltips()
    {
        switch (currentTooltip)
        {
            case 1:
                if (CheckTooltip1())
                {
                    AutoFade.LoadLevel(9, .3f, .3f, Color.black);
                }
                break;

            case 2:
                if (!CheckTooltip1())
                {
                    myHUD.SetObjective("Achever la souris intéligente");
                    currentTooltip++;
                }
                break;
        }
    }

    /*
	 * Objectif: tuer une souris maligne
	 */
    bool CheckTooltip1()
    {
        List<GameObject> ListCatEnemy = myHUD.player.nearEnemy;
        bool stop = false;
        foreach (GameObject enemy in ListCatEnemy)
        {
            Mouse mouse = enemy.GetComponent<Mouse>();
            if (mouse != null)
            {
                if (mouse.mouseType == MouseType.LVL2)
                {
                    stop = true;
                    break;
                }
            }
        }
        return stop;
    }
}
