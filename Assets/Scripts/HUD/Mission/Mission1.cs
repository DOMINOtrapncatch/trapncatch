using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Mission1
{
    HUDManager myHUD;
    public Mouse mouse;
    public Cat player;
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
        //position du chat au dessus de la poubelle
        //spawn le chat a cet endroit 
        //sinon on s'en ballec vu qu'il y'a le bug immobilisant
        myHUD.SetObjective("Attaquez la souris");
	}
	
	// Update is called once per frame
	void Update () {
	    /*if(CheckTooltip1())
        {
            myHUD.SetObjective("Achevez la souris(Coup Special)");
            if (mouse.life <=0 && !CheckTooltip2())
            {
                mouse.life = 1;//freez pv de la souris a 1 tant que il utilise pas une att sppeciale
            }
            else if(CheckTooltip2() && mouse.life == 0)//test si il a bien fatality la souris 
            {
                //win
                //retour menu selection chapitre
            }
        }*/
	}

    bool CheckTooltip1()
    {
        if (mouse.maxLife != mouse.life)//aka elle s'est faite attaquer au moins une fois
        {
            return true;
        }

        return false;
    }

    bool CheckTooltip2()
    {
        //on va check si le mana du chat a diminué au moins une fois
        if(player.maxMana != player.mana)
        {
            if(mouse.life == 0 || mouse.life ==1)//pour gerer des bugs eventuels tmtc
            {
                return true;
            }
        }

        return false;
    }

}
