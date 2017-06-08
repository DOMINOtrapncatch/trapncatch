using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Collections.Generic;

public class Mode1_Kill : NetworkBehaviour {

    /*
     * spaw nbr mouse
     * get list of nbr mouse and deathcounter
     * despawn when killed and check with death counter
     * deathcounter ok = finish game
     * 
     * each cat has their own death counter
     * sort for the biggest one -> winner
     * 
     * 
     */
    MHUDManager myHUD;
    public Image healthBar;
    public Image manaBar;
    public Image spellBar1;
    public Image spellBar2;
    public Image spellBar3;
    public Image spellBar4;
    public Text objective;

    public MCat player;
    public string toolTip = "Tuer toutes les souris";
    private List<MMouse> mice_list;

    void Start()
    {
        myHUD = new MHUDManager(player, healthBar, manaBar, spellBar1, spellBar2, spellBar3, spellBar4, objective);
        myHUD.SetObjective(toolTip);
        mice_list = new List<MMouse>();
    }

    void Update()
    {
        myHUD.UpdateAll();
        
    }
}
