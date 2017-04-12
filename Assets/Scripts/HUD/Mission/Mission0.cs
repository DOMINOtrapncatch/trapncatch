using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Mission0 : MonoBehaviour
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
    public BoxCollider box;



    int tooltip1;// 4 -> true, qu'on a test toutes les touches dir ou ZQSD
    int tooltip2;//parce que les booleen ça me soule
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
         
        //en fonction des checktooltip validés, on affiche les tooltip suivants
        //c'pas opti a modif
        if (CheckTooltip1(ref tooltip1) >= 4)
        {
            myHUD.SetObjective("Left Click pour charger une attaque");
            if(CheckTooltip2(ref tooltip2) >= 1)
            {
                myHUD.SetObjective("Poursuivre la souris");
                
                //pour le reste check istrigger
            }
        } 
    }

    int CheckTooltip1(ref int tooltip1)
    {
        if(Input.GetKeyDown(KeyCode.Z))
        {
            ++tooltip1;
        }
        else if(Input.GetKeyDown(KeyCode.Q))
        {
            ++tooltip1;
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            ++tooltip1;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            ++tooltip1;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            ++tooltip1;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            ++tooltip1;
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            ++tooltip1;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            ++tooltip1;
        }

        return tooltip1;
    }

    int CheckTooltip2(ref int tooltip2)
    {
        //ne pas oublier de changer la touche E pour un left click
        // synchro ces changements sur le script hud egalement
        if(Input.GetKeyDown(KeyCode.E))
        {
            ++tooltip2;
        }

        return tooltip2;
        
    }

    bool CheckTooltip3()
    {
        //pourchassez la souris jusqu'a un point B

        //si collider chat is triggered by collider souris
        return true;
    }

  
    void OnTriggerEnter(Collider box)
    {
        // se rendre jusqu'a a porte -> loading scene menu et debloque chap 1
        //triggerd le collider sur la porte !
        Debug.Log("Amandine - "+ box.tag);
        if(box.tag == "Collider" && tooltip3)
        {
            //changescene here
            myHUD.SetObjective("test is OK");
        }

        else if(box.tag == "Mouse")
        {
            tooltip3 = true;
            myHUD.SetObjective("Se rendre jusqu'à la porte");
        }
        
    }

   

   


}
