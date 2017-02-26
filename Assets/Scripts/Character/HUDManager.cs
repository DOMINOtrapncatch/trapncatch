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

    public void Update()
    {
        Update(objectiveUI.text);
    }

    public void Update(string objective)
    {
        statusUI[0].fillAmount = player.life / player.maxLife;
        // statusUI[1].fillAmount = player.mana / player.maxMana;
        // TODO: add cooldown
        objectiveUI.text = objective;
    }
}
