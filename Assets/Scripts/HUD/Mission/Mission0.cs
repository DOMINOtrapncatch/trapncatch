using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Mission0
{
    HUDManager myHUD;
    public Cat player;
    public Image healthBar;
    public Image manaBar;
    public Image spellBar1;
    public Image spellBar2;
    public Image spellBar3;
    public Image spellBar4;
    public Text objective;

    bool tooltip1;
    bool tooltip3;
    bool tooltip4;

    // Use this for initialization
    void Start()
    {
        // Une classe ça s'instancie amandine !
        myHUD = new HUDManager(player, healthBar, manaBar, spellBar1, spellBar2, spellBar3, spellBar4, objective);
        //premier tooltip
        myHUD.SetObjective("Se déplacer (ZQSD)");
    }

    // Update is called once per frame
    void Update()
    {
        myHUD.UpdateSpell();
        myHUD.UpdateHealth();
        myHUD.UpdateMana();
         /*
        //en fonction des checktooltip validés, on affiche les tooltip suivants
        //c'pas opti a modif
        if (CheckTooltip1())
        {
            myHUD.SetObjective("Clic gauche pour charger une attaque");
            if(CheckTooltip2())
            {
                myHUD.SetObjective("Poursuivre la souris");
                if(CheckTooltip3())
                {
                    myHUD.SetObjective("Se rendre jusqu'à la porte");
                    if(CheckTooltip4())
                    {
                        //win
                        //passage au chapitre suivant 
                    }
                }
            }
        } */
    }

    bool CheckTooltip1()
    {
        if(Input.GetKeyDown(KeyCode.Z) || 
            Input.GetKeyDown(KeyCode.Q) || 
            Input.GetKeyDown(KeyCode.S) || 
            Input.GetKeyDown(KeyCode.D) || 
            Input.GetKeyDown(KeyCode.UpArrow) ||
            Input.GetKeyDown(KeyCode.DownArrow) ||
            Input.GetKeyDown(KeyCode.LeftArrow) ||
            Input.GetKeyDown(KeyCode.RightArrow) )
        {
            return tooltip1 &= true;
        }

        return tooltip1 &= false;
    }

    bool CheckTooltip2()
    {
        if(Input.GetMouseButton(0))//left click
            return true;
        return false;
    }

    bool CheckTooltip3()
    {
        //pourchassez la souris jusqu'a un point B

        //si collider chat is triggered by collider souris
        return true;
    }

    bool CheckTooltip4()
    {
        // se rendre jusqu'a a porte -> loading scene menu et debloque chap 1

        //when vector3 chat  == vector3 position de la sortie 
        return true;
    }


}
