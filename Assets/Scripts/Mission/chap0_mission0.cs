using UnityEngine;
using System.Collections;

public class chap0_mission0 : MonoBehaviour {

    public HUDManager hudmanager;
    public Cat cat;
    public Input input;

    public bool tooltip1;
    
    public bool tooltip3;
    public bool tooltip4;
    // Use this for initialization
    void Start()
    {
        //premier tooltip
        hudmanager.SetObjective("Se déplacer (T.D. ou ZQSD)");

    }

    // Update is called once per frame
    void Update()
    {
        //en fonction des checktooltip validés, on affiche les tooltip suivants
        //c'pas opti a modif
        if(CheckTooltip1())
        {
            hudmanager.SetObjective("Clic gauche pour charger une attaque");
            if(CheckTooltip2())
            {
                hudmanager.SetObjective("Poursuivre la souris");
                if(CheckTooltip3())
                {
                    hudmanager.SetObjective("Se rendre jusqu'à la porte");
                    if(CheckTooltip4())
                    {
                        //win
                        //passage au chapitre suivant 
                    }
                }
            }
        }
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
        {
            return true;
        }
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
