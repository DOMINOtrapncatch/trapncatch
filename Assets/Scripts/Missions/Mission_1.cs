using System;
using UnityEngine;

public class Mission_1 : Mission
{
	override public void CheckTooltips()
	{
		switch (currentTooltip)
		{
			case 0:
				if (CheckTooltip0())
				{
					myHUD.SetMission(1);
					currentTooltip++;

					myHUD.player.Attack = 0; // Pevent from killing without using mana
				}
				break;

			case 1:
				if (CheckTooltip1())
				{
					myHUD.SetMission(2);
					currentTooltip++;

					myHUD.player.Attack = 10000; // Allow to one shot
				}
				break;

			case 2:
				if (CheckTooltip2())
					HistoryMenu.MissionSuccess();
				break;
		}
	}

	/*
     * Objectif: blesser une souris
     */
	bool CheckTooltip0()
	{
		if (myHUD.player.isMovingAttack && myHUD.player.nearEnemy.Count > 0)
        {
			myHUD.SetMissionAdvancementBar(1, 1);
            return true;
        }

        return false;
	}

	/*
     * Objectif: utiliser un sort
     */
	bool CheckTooltip1()
	{
		if (myHUD.player.spellsUseCount > 0)
			myHUD.SetMissionAdvancementBar(1, 1);
        
		return myHUD.player.spellsUseCount > 0;
	}

	/*
     * Objectif: tuer la souris
     */
	bool CheckTooltip2()
	{
		if (myHUD.player.enemyKillCount > 0)
			myHUD.SetMissionAdvancementBar(1, 1);
        
		return myHUD.player.enemyKillCount > 0;
	}
}
