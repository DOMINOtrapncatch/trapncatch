using UnityEngine;
using System.Collections;

public class Mission3 : MissionBase
{ 
    [Header("Tooltips Configuration")]
    public string tooltip2 = "Tuez une dizaine de souris.";

    private Cat firstPlayer;
    private Cat secondPlayer;
    private bool hasBeenInitialized = false;

    override public void CheckTooltips()
    {
        // Set the starting player
        if (!hasBeenInitialized)
        {
            firstPlayer = myHUD.player;
            hasBeenInitialized = true;
        }

        switch (currentTooltip)
        {
            case 1:
                if (CheckTooltip1())
                {
                    myHUD.SetObjective(tooltip2);
                    ++currentTooltip;
                }
                break;

            case 2:
                if (CheckTooltip2())
                {
                    AutoFade.LoadLevel(9, .3f, .3f, Color.black);
                    SaveManager.Set("mission3", "1");
                }
                break;
        }
    }

    // Check if swap has been used !
    bool CheckTooltip1()
    {
        if (Swap.HasSwapped)
            secondPlayer = myHUD.player;
        return Swap.HasSwapped;
    }

    bool CheckTooltip2()
    {
        return firstPlayer.enemyKillCount + secondPlayer.enemyKillCount >= 10;
    }
}
