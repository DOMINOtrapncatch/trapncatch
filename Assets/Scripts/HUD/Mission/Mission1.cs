using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Mission1 : MissionBase
{
	// Tooltips variables
	public string messageTooltip2 = "Achever la souris";
    int tooltip1;
    int tooltip2;

	override public void CheckTooltips()
	{
		switch(currentTooltip)
		{
			case 1:
				if(CheckTooltip1())
		        {
					myHUD.SetObjective(messageTooltip2);
					currentTooltip++;
		        }
				break;

			case 2:
				if(CheckTooltip2())
	            {
	                AutoFade.LoadLevel(9, .3f, .3f, Color.black);
	            }
				break;
		}
	}

	/*
	 * Objectif: blesser une souris au moins une fois
	 */
    bool CheckTooltip1()
    {
		if (Input.GetButtonDown("attack") && myHUD.player.nearEnemy.Count > 0)
		{
			if (tooltip1 == 0)
				myHUD.player.Attack = 0;

			tooltip1 = 1;
		}

        return tooltip1 == 1;
    }

	/*
	 * Objectif: tuer la souris en utilisant du mana
	 */
    bool CheckTooltip2()
    {
		if(myHUD.player.spellUseCount > 0)
		{
			myHUD.player.Attack = 10000;
        	++tooltip2;
		}

		return tooltip2 >= 1 && myHUD.player.enemyKillCount > 0;
    }

}
