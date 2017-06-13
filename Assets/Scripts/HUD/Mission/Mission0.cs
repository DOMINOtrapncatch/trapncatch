using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Mission0 : MissionBase
{
	// Tooltips variables
	public string messageTooltip2 = "Left Click pour attaquer";
	public string messageTooltip3 = "Poursuivre la souris";
    public string messageTooltip4 = "Aller jusqu'à la porte";
	int tooltip1 = 0;
	int tooltip2 = 0;
    int tooltip3 = 0;

	override public void CheckTooltips()
	{
		switch(currentTooltip)
		{
			case 1:
				myHUD.player.Attack = 0;
				if (CheckTooltip1(true))
				{
					myHUD.SetObjective(messageTooltip2);
					currentTooltip++;
				}
				break;

			case 2:
				if(CheckTooltip2(true))
				{
					myHUD.SetObjective(messageTooltip3);
					currentTooltip++;
				}
				break;

			case 3:
				if(CheckTooltip3())
				{
					myHUD.SetObjective(messageTooltip4);
					currentTooltip++;
				}
				break;

			case 4:
				if(CheckTooltip4())
				{
                    ChooseMission.Success();
                    SaveManager.Set("mission0", "1");
				}
				break;
		}
	}

	/*
	 * Objectif: utiliser au moins 4 fois les touches de direction
	 */
	bool CheckTooltip1(bool checkInputs)
	{
		if(checkInputs && (Input.GetButtonDown("up") || Input.GetButtonDown("right") || Input.GetButtonDown("down") || Input.GetButtonDown("left")))
			++tooltip1;

		return tooltip1 >= 4;
	}

	/*
	 * Objectif: attaquer au moins une fois
	 */
	bool CheckTooltip2(bool checkInputs)
	{
		if(checkInputs && Input.GetButtonDown("attack")) // Check for real attack --> && player.nearEnemy.Count > 0)
		{
			if(tooltip2 == 0)
				myHUD.player.Attack = 0;
			
			tooltip2 = 1;
		}

		return tooltip2 == 1;
	}

	/*
	 * Objectif: poursuivre la souris
	 */
	bool CheckTooltip3()
	{
		if (myHUD.player.nearEnemy.Count > 1)
			++tooltip3;

		return tooltip3 >= 100;
	}

	/*
	 * Objectif: aller vers la porte d'entrée
	 */
	bool CheckTooltip4()
	{
		return myHUD.player.nearColliders.Count >= 1;
	}
}