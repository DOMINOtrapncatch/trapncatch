using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Mission2 : MonoBehaviour
{
	// HUD Handling
    HUDManager myHUD;
    public Image healthBar;
    public Image manaBar;
    public Image spellBar1;
    public Image spellBar2;
    public Image spellBar3;
    public Image spellBar4;
    public Text objective;

	// Player related variables
    public Cat player;

	// Tooltips variables
	public string messageTooltip1 = "Tuer toutes les souris";

    // Use this for initialization
    void Start ()
    {
        myHUD = new HUDManager(player, healthBar, manaBar, spellBar1, spellBar2, spellBar3, spellBar4, objective);
		myHUD.SetObjective(messageTooltip1);
    }
	
	// Update is called once per frame
	void Update ()
	{
		myHUD.UpdateAll();

        CheckTooltips();
	}

	void CheckTooltips()
	{
		if(CheckTooltip1())
        {
			AutoFade.LoadLevel(9, .3f, .3f, Color.black);
        }
	}

	/*
	 * Objectif: tuer un nombre de souris fixées
	 */
    bool CheckTooltip1()
    {
		return player.enemyKillCount >= 4;
    }

}
