using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

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
    public Text scoreText;

    public string toolTip = "Tuer toutes les souris";
    private List<MMouse> mice_list;
    private List<MCat> player_list;
    private int score;

    public override void OnStartLocalPlayer()
    {
        enabled = true;
    }

    
    void Awake()
    {
        scoreText = transform.Find("Score").GetComponent<Text>();
        GameObject[] tempMouse = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject[] tempPlayer = GameObject.FindGameObjectsWithTag("Cat");
        

        mice_list = new List<MMouse>();
        player_list = new List<MCat>();

        for(int i = 0; i < tempMouse.Count(); ++i)
        {
            mice_list.Add(tempMouse[i].GetComponent<MMouse>());
        }

        for (int i = 0; i < tempPlayer.Count(); ++i)
        {
            player_list.Add(tempPlayer[i].GetComponent<MCat>());
        }
    }

    void Start()
    {
        
       
    }


    
    void Update()
    {
        if (!isLocalPlayer)
            return;

       if(mice_list.Count > 0)
        {
            KillThemAll();
        }

       if(mice_list.Count <= 0 || player_list.Count <= 1)
        {
            LoadKTAEndScreen();
        }
    }

    public void KillThemAll()
    {
        foreach(MMouse mice in mice_list)
        {
            if(mice.Life <= 0)
            {
                mice_list.Remove(mice);
                //le code dans MCat se charge du reste
                //score and despawning

            }
        }
    }

    //[Server]//modifiable
    public MCat WhoWin()
    {
        if(player_list.Count > 0)
        {
            player_list.Sort(delegate (MCat a, MCat b) { return b.score.CompareTo(a.score); });
            return player_list[0];
        }

        return null;
    }

    [Server]//modifiable
    public void LoadKTAEndScreen()
    {
        MCat winner = WhoWin();
        foreach(MCat player in player_list)
        {
            if(player == winner)
            {
                //load win screen
            }
            else
            {
                //load gameover
            }
        }
    }

    [Server]
    public void AddScore()
    {
        RpcAddScore();
    }

    [ClientRpc]
    void RpcAddScore()
    {
        if (isLocalPlayer)
        {
            //add point to score variable selon type souris
            myHUD.DisplayScore(score);
        }
    }
}
