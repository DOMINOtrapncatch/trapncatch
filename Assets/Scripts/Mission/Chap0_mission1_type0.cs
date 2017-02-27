using UnityEngine;
using System.Collections;

public class Chap0_mission1_type0 : MonoBehaviour {

    public Transform player;
    private AverageCat classicat;

    public Transform tooltip1;
    public Transform tooltip2;
    public Transform tooltip3;
    public Transform tooltip4;

	// Use this for initialization
	void Start ()
    {
	    //premier tooltip
	}
	
	// Update is called once per frame
	void Update ()
    {
	    //en fonction des checktooltip validés, on affiche les tooltip suivants
	}

    bool CheckTooltip1()
    {
        // actions de mouvement
        
        return true;
    }

    bool CheckTooltip2()
    {
        //charger une attaque
        return true;
    }

    bool CheckTooltip3()
    {
        //pourchassez la souris jusqu'a un point B
        return true;
    }

    bool CheckTooltip4()
    {
        // se rendre jusqu'a a porte -> loading scene menu et debloque chap 1
        return true;
    }

    
}
