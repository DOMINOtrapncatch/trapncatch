using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class HUDManager
{
    Cat player;
    List<Image> statusUI;
    List<Image> spellsUI;
    Text objectiveUI;

    public HUDManager(Cat player, Image healthUI, Image manaUI, Image spell1, Image spell2, Image spell3, Image spell4, Text objectiveUI)
    {
		this.player = player;

        this.statusUI = new List<Image>();
        this.spellsUI = new List<Image>();

        this.player = player;
        this.objectiveUI = objectiveUI;

		this.statusUI.Add(healthUI);
        this.statusUI.Add(manaUI);

		if (player.spells.Count >= 1)
			this.spellsUI.Add(spell1);
		else
			spell1.transform.parent.gameObject.SetActive(false);
		
		if(player.spells.Count >= 2)
            this.spellsUI.Add(spell2);
		else
			spell2.transform.parent.gameObject.SetActive(false);
		
		if(player.spells.Count >= 3)
            this.spellsUI.Add(spell3);
		else
			spell3.transform.parent.gameObject.SetActive(false);
		
		if(player.spells.Count >= 4)
            this.spellsUI.Add(spell4);
		else
			spell4.transform.parent.gameObject.SetActive(false);

		UpdateHealth();
		UpdateSpell();
    }

	public void UpdateAll()
	{
		UpdateSpell();
		UpdateHealth();
		UpdateMana();
	}

    private void UpdateSpell()
	{
		for(int i = 0; i < player.spells.Count; i++)
		{
			if (player.spells[i].recoveryTime < player.spells[i].maxRecoveryTime)
			{
				player.spells[i].RecoveryTime += 1;
				spellsUI[i].color = new Color(spellsUI[i].color.r, spellsUI[i].color.g, spellsUI[i].color.b, 20);
			}
			else
			{
				spellsUI[i].color = new Color(spellsUI[i].color.r, spellsUI[i].color.g, spellsUI[i].color.b, 100);
			}
			
			spellsUI[i].fillAmount = player.spells[i].recoveryTime / player.spells[i].maxRecoveryTime;
		}
    }

    public void SetObjective(string objective)
    {
        objectiveUI.text = objective;
    }

    private void UpdateHealth()
    {
		statusUI[0].fillAmount = player.life / player.maxLife;
    }

	private void UpdateMana()
    {
		statusUI[1].fillAmount = player.mana / player.maxMana;

		if (player.mana < player.maxMana)
		{
			if(player.ManaRecoveryTime == player.manaMaxRecoveryTime)
			{
				player.ManaRecoveryTime = 0;
				player.Mana += 1;
			}
			else
			{
				player.ManaRecoveryTime += 1;
			}
		}
    }
}