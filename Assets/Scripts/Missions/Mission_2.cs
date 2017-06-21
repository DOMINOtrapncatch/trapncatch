public class Mission_2 : Mission
{
	override public void CheckTooltips()
	{
		switch (currentTooltip)
		{
			case 0:
				if (CheckTooltip0())
					HistoryMenu.MissionSuccess();
				break;
		}
	}

	/*
     * Objectif: Attaper la souris intelligente
     */
	bool CheckTooltip0()
	{
        if(myHUD.player.isMovingAttack && myHUD.player.nearEnemy.Count > 0)
        {
            foreach(Character character in myHUD.player.nearEnemy)
            {
                Mouse mouse = character.gameObject.GetComponent<Mouse>();

                if(mouse != null && mouse.mouseType == MouseType.SMART)
                    myHUD.SetMissionAdvancementBar(mouse.MaxLife - mouse.Life, mouse.MaxLife);
            }
        }

        return myHUD.player.GetComponent<Cat>().smartMouseKillCount == 1;
	}
}
