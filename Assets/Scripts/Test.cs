using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class Test : MonoBehaviour
{
    public Cat player;
    HUDManager myHUD;
    public Image healthBar;
    public Image manaBar;
    public Image spellBar1;
    public Image spellBar2;
    public Image spellBar3;
    public Image spellBar4;
    public Text objective;

    // Use this for initialization
    void Start ()
    {
		myHUD = new HUDManager(player, healthBar, manaBar, spellBar1, spellBar2, spellBar3, spellBar4, objective);
        myHUD.SetObjective("Bonjour !");
    }
	
	// Update is called once per frame
	void Update ()
    {
		myHUD.UpdateSpell ();
		myHUD.UpdateHealth();
		myHUD.UpdateMana();
	}
}
