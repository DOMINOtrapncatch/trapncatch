using UnityEngine;

public class Mission_4 : Mission
{
    override public void CheckTooltips()
    {
        switch (currentTooltip)
        {
            case 1:
                if (CheckTooltip0())
                    HistoryMenu.MissionSuccess();
                break;
        }
    }

	/*
     * Objectif: tuer une dizaine de souris
     */
	bool CheckTooltip0()
    {
        int enemyKillCountTotal = 0;
        
        foreach(Character character in myHUD.players)
            enemyKillCountTotal += character.enemyKillCount;

        myHUD.SetMissionAdvancementBar(enemyKillCountTotal, 10);

        return enemyKillCountTotal >= 10;
    }
}
