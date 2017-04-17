using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class HUDManager : MonoBehaviour
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

        this.spellsUI.Add(spell1);
        this.spellsUI.Add(spell2);
        this.spellsUI.Add(spell3);
        this.spellsUI.Add(spell4);

		UpdateHealth();
		UpdateMana();
    }

    public void UpdateSpell()
	{
		for(int i = 0; i < player.spells.Count; i++)
		{
			if (player.spells[i].recoveryTime < player.spells[i].maxRecoveryTime)
			{
				player.spells[i].recoveryTime += 1;
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

    public void UpdateHealth()
    {
		statusUI[0].fillAmount = player.life / player.maxLife;
    }

	public void UpdateMana()
    {
		statusUI[1].fillAmount = player.mana / player.maxMana;
		player.mana += 0.1f;
    }
}