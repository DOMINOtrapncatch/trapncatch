﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Mission0 : MonoBehaviour
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
	public string messageTooltip1 = "Se déplacer";
	public string messageTooltip2 = "Left Click pour attaquer";
	public string messageTooltip3 = "Poursuivre la souris";
    public string messageTooltip4 = "Aller jusqu'à la porte";
	int tooltip1 = 0;
	int tooltip2 = 0;
    int tooltip3 = 0;

	// Use this for initialization
	void Start()
	{
		myHUD = new HUDManager(player, healthBar, manaBar, spellBar1, spellBar2, spellBar3, spellBar4, objective);
		myHUD.SetObjective(messageTooltip1);
	}

	// Update is called once per frame
	void Update()
	{
		myHUD.UpdateAll();

		CheckTooltips();
	}

	void CheckTooltips()
	{
		if (CheckTooltip1(true) && !CheckTooltip3())
		{
			myHUD.SetObjective(messageTooltip2);

			if(CheckTooltip2(true))
			{
				myHUD.SetObjective(messageTooltip3);
			}
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
			if(tooltip2 > 0)
				player.Attack = 0;
			
			++tooltip2;
		}

		return tooltip2 >= 1;
	}

	/*
	 * Objectif: poursuivre la souris
	 */
	bool CheckTooltip3()
	{
		return tooltip3 >= 4;
	}

	void OnTriggerEnter(Collider box)
	{
		if (!CheckTooltip3() && box.tag == "Enemy" && CheckTooltip2(false))
        {
            myHUD.SetObjective(messageTooltip4);
			++tooltip3;
        }
		else if (box.tag == "Collider" && CheckTooltip3())
		{
			AutoFade.LoadLevel(9, .3f, .3f, Color.black); 
		}
	}
}