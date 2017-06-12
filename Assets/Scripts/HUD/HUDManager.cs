using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class HUDManager : MonoBehaviour
{
    public Cat player;
    List<Image> statusUI = new List<Image>();
    List<Image> spellsUI = new List<Image>();
    Text objectiveUI;

    void Awake()
    {
        this.objectiveUI = transform.Find("StatusBox/Panel/Text").GetComponent<Text>();

        this.statusUI.Add(transform.Find("StatusBox/Status/Health/HealthBar").GetComponent<Image>());
        this.statusUI.Add(transform.Find("StatusBox/Status/Mana/ManaBar").GetComponent<Image>());

        for (int i = 1; i <= 4; i++)
            this.spellsUI.Add(transform.Find("RadialKey" + i + "/LoadingBar").GetComponent<Image>());

        LoadPlayer(player);

        UpdateHealth();
        UpdateSpell();
    }

    void Update()
    {
        UpdateSpell();
        UpdateHealth();
        UpdateMana();
    }

    public void LoadPlayer(Cat cat)
    {
        this.player = cat;

        for (int i = 1; i <= (cat.spells.Count > 4 ? 4 : cat.spells.Count); i++)
        {
            Image spellImage = transform.Find("RadialKey" + i + "/Center/Image").GetComponent<Image>();

            if (cat.spells.Count >= i)
            {
                spellImage.sprite = player.spells[i - 1].image;
                spellsUI[i - 1].transform.parent.gameObject.SetActive(true);
            }
            else
                spellsUI[i - 1].transform.parent.gameObject.SetActive(false);
        }
    }

    private void UpdateSpell()
    {
        for (int i = 0; i < player.spells.Count; i++)
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
            if (player.ManaRecoveryTime == player.manaMaxRecoveryTime)
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