using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Test : MonoBehaviour
{
    BlackCat player;
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
        player = new BlackCat(0, 0, 0);
        myHUD = new HUDManager(player, healthBar, manaBar, spellBar1, spellBar2, spellBar3, spellBar4, objective);
        player.life = 100;
        myHUD.SetObjective("Approchez vous de la souris.");
    }
	
	// Update is called once per frame
	void Update ()
    {
        
	}
}
