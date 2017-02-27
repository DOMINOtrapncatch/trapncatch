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
        this.statusUI = new List<Image>();
        this.spellsUI = new List<Image>();

        this.player = player;
        this.objectiveUI = objectiveUI;

        this.statusUI.Add(healthUI);
        this.statusUI.Add(manaUI);

        this.spellsUI.Add(spell1);
        this.spellsUI.Add(spell1);
        this.spellsUI.Add(spell1);
        this.spellsUI.Add(spell1);
    }

    public void UpdateSpell()
    {
        spellsUI[0].fillAmount = player.spell_list[1].recovery_time / player.spell_list[1].recovery_max;
        spellsUI[0].fillAmount = player.spell_list[2].recovery_time / player.spell_list[2].recovery_max;
        spellsUI[0].fillAmount = player.spell_list[3].recovery_time / player.spell_list[3].recovery_max;
        spellsUI[0].fillAmount = player.spell_list[4].recovery_time / player.spell_list[4].recovery_max;
    }

    public void SetObjective(string objective)
    {
        objectiveUI.text = objective;
    }

    public void SetHealth()
    {
        statusUI[0].fillAmount = player.life / player.maxLife;
    }

    public void SetMana()
    {
        statusUI[0].fillAmount = player.mana / player.maxMana;
    }
}
