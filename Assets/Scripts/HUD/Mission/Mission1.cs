using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Mission1 : MonoBehaviour
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
    private int tooltip1;
    private int tooltip2;

    // Use this for initialization
    void Start ()
    {
        myHUD = new HUDManager(player, healthBar, manaBar, spellBar1, spellBar2, spellBar3, spellBar4, objective);
        tooltip1 = 0;
        tooltip2 = 0;
        //position du chat au dessus de la poubelle
        //spawn le chat a cet endroit 
        //sinon on s'en ballec vu qu'il y'a le bug immobilisant
        myHUD.SetObjective("Attaquez la souris");
	}
	
	// Update is called once per frame
	void Update () {
	    if(CheckTooltip1(ref tooltip1) >= 1)
        {
            myHUD.SetObjective("Achevez la souris (E)");
            if (mouse.life <= 0 && CheckTooltip2(ref tooltip2) == 0) //if on kill la souris sans capacité special
            {
				mouse.life = mouse.maxLife;//freez pv de la souris a 100 tant que il utilise pas une att sppeciale


            }
            else if(CheckTooltip2(ref tooltip2) >=1 && mouse.life <= 0)//test si il a bien fatality la souris 
            {
                //win
                //retour menu selection chapitre

            }
        }
	}

    int CheckTooltip1(ref int tooltip1)
    {
        if (mouse.maxLife != mouse.life)//aka elle s'est faite attaquer au moins une fois
        {
            ++tooltip1;
            
        }

        return tooltip1;
    }

    int CheckTooltip2(ref int tooltip2)
    {
        //on va check si le mana du chat a diminué au moins une fois
        if(player.maxMana != player.mana)
        {
            ++tooltip2;
        }

        return tooltip2;
    }

}
