using UnityEngine;
using System.Collections;

public class chap1_mission1 : MonoBehaviour {
    public Cat cat;
    public Mouse mouse;
    public HUDManager hudmanager;
	// Use this for initialization
	void Start () {
        //position du chat au dessus de la poubelle
        //spawn le chat a cet endroit 
        //sinon on s'en ballec vu qu'il y'a le bug immobilisant
        hudmanager.SetObjective("Attaquez sur la souris");
	}
	
	// Update is called once per frame
	void Update () {
	    if(CheckTooltip1())
        {
            hudmanager.SetObjective("Achevez la souris(Coup Special)");
            if (mouse.life <=0 && !CheckTooltip2())
            {
                mouse.life = 1;//freez pv de la souris a 1 tant que il utilise pas une att sppeciale
            }
            else if(CheckTooltip2() && mouse.life == 0)//test si il a bien fatality la souris 
            {
                //win
                //retour menu selection chapitre
            }
        }
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
        if(cat.maxMana != cat.mana)
        {
            if(mouse.life == 0 || mouse.life ==1)//pour gerer des bugs eventuels tmtc
            {
                return true;
            }
        }

        return false;
    }

}
