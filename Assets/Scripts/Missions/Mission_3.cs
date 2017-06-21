using UnityEngine;

public class Mission_3 : Mission
{    
    public override void OnLoad()
    {
		foreach (Character character in myHUD.players)
            character.Attack = 0;
    }

    override public void CheckTooltips()
    {
        switch (currentTooltip)
        {
            case 0:
                if (CheckTooltip0())
                {
                    myHUD.SetMission(1);
                    ++currentTooltip;

					foreach (Character character in myHUD.players)
						character.AttackPercentage = 100;
                }
                break;

            case 1:
                if (CheckTooltip1())
                    HistoryMenu.MissionSuccess();
                break;
        }
    }

	/*
     * Objectif: utiliser le swap
     */
	bool CheckTooltip0()
    {
        return myHUD.hasSwapped;
    }

	/*
     * Objectif: tuer une dizaine de souris
     */
	bool CheckTooltip1()
    {
        int enemyKillCountTotal = 0;
        
        foreach(Character character in myHUD.players)
            enemyKillCountTotal += character.enemyKillCount;

        myHUD.SetMissionAdvancementBar(enemyKillCountTotal, 10);

        return enemyKillCountTotal >= 10;
    }
}
