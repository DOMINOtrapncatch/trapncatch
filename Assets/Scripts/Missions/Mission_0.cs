using UnityEngine;

public class Mission_0 : Mission
{
    // Tooltips variables
	int tooltip1 = 0;
	int tooltip2 = 0;
	int tooltip3 = 0;

    override public void OnLoad()
    {
        myHUD.player.Attack = 0;
    }

	override public void CheckTooltips()
	{
		switch (currentTooltip)
		{
			case 0:
				if (CheckTooltip0())
				{
					myHUD.SetMission(1);
					currentTooltip++;
				}
				break;

			case 1:
				if (CheckTooltip1())
				{
					myHUD.SetMission(2);
					currentTooltip++;
				}
				break;

			case 2:
				if (CheckTooltip2())
				{
					myHUD.SetMission(3);
					currentTooltip++;
				}
				break;

			case 3:
				if (CheckTooltip3())
                    HistoryMenu.MissionSuccess();
				break;
		}
	}

	/*
     * Objectif: utiliser au moins 4 fois les touches de direction
     */
	bool CheckTooltip0()
	{
        if(myHUD.player.isMovingForward || myHUD.player.isMovingRight || myHUD.player.isMovingBackward || myHUD.player.isMovingLeft)
            ++tooltip1;

        myHUD.SetMissionAdvancementBar(tooltip1, 8);
		return tooltip1 == 8;
	}

	/*
     * Objectif: attaquer au moins une fois
     */
	bool CheckTooltip1()
	{
        if (myHUD.player.isMovingAttack) // Check for real attack --> && player.nearEnemy.Count > 0)
			tooltip2 += 1;

        myHUD.SetMissionAdvancementBar(tooltip2, 3);
		return tooltip2 == 3;
	}

	/*
     * Objectif: poursuivre la souris
     */
	bool CheckTooltip2()
	{
		if (myHUD.player.nearEnemy.Count >= 1)
			++tooltip3;

        myHUD.SetMissionAdvancementBar(tooltip3, 100);
		return tooltip3 == 100;
	}

	/*
     * Objectif: aller vers la porte d'entrée
     */
	bool CheckTooltip3()
	{
        if(myHUD.player.nearColliders.Count >= 1)
            myHUD.SetMissionAdvancementBar(1, 1);
        
		return myHUD.player.nearColliders.Count >= 1;
	}
}
