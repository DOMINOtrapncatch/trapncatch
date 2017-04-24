using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Mission1 : MonoBehaviour
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
	public string messageTooltip1 = "Attaquez la souris";
	public string messageTooltip2 = "Achever la souris";
    int tooltip1;
    int tooltip2;

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
			myHUD.SetObjective(messageTooltip2);

            if(CheckTooltip2())
            {
                AutoFade.LoadLevel(9, .3f, .3f, Color.black);
            }
        }
	}

	/*
	 * Objectif: blesser une souris au moins une fois
	 */
    bool CheckTooltip1()
    {
		if (Input.GetButtonDown("attack") && player.nearEnemy.Count > 0)
		{
			if (tooltip1 > 0)
				player.Attack = 0;

			++tooltip1;
		}

        return tooltip1 >= 1;
    }

	/*
	 * Objectif: tuer la souris en utilisant du mana
	 */
    bool CheckTooltip2()
    {
        if(player.maxMana != player.mana)
		{
			player.Attack = 10000;
        	++tooltip2;
		}

		return tooltip2 >= 1 && player.enemyKillCount > 0;
    }

}
