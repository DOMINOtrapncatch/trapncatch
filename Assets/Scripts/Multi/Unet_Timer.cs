using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Unet_Timer : NetworkBehaviour {

    //-1 = attente de joueurs
    //-2 = fin du jeu

        /*
         * time not sync -> parce que mes appels faut QUE JE LES METTE AU BON ENDROIT HEIN
         * et wtf que chez les clients le canvas se deplace genre ok 
         */

    [SyncVar]
    public int minPlayers;
    public bool masterTimer = false;
   
    public Timer countdown;

    private Unet_Timer serverTimer;
    private Unet_Timer[] timers;

    void Start()
    {
        countdown = GetComponent<Timer>();
        if(isServer)
        {
            if(isLocalPlayer)
            {
                serverTimer = this;
                masterTimer = true;
            }
        }
        else if(isLocalPlayer)
        {
            timers = FindObjectsOfType<Unet_Timer>();
            for(int i = 0; i < timers.Length; ++i)
            {
                if (timers[i].masterTimer)
                {
                    serverTimer = timers[i];
                }
            }
        }
    }

    void Update()
    {

        countdown.TimeControl();
        countdown.LastMinuteRed();

        //ya que le server qui touche directement au temps
        MasterControl();

        if(isLocalPlayer)
        {
            //tout le monde update
            ClientTimeUpdate();
            countdown.TimeControl();
            countdown.LastMinuteRed();
        }

        
    }

    

    public void MasterControl()
    {
        if(masterTimer)
        {
            if(countdown.timelimit <= 0)
            {
                countdown.timelimit = -2;
            }
            else if(countdown.timelimit == -1)
            {
                if (NetworkServer.connections.Count >= minPlayers)
                    countdown.timelimit = 0;
            }
            else if(countdown.timelimit == -2)
            {
                // fin du jeu
                //load scene
            }
        }
    }

    public void ClientTimeUpdate()
    {
        if(serverTimer)
        {
            countdown.timelimit = serverTimer.countdown.timelimit;
            minPlayers = serverTimer.minPlayers;
        }
        else
        {
            //Unet_Timer[] timers = FindObjectsOfType<Unet_Timer>();
            for(int i = 0; i < timers.Length; ++i)
            {
                if(timers[i].masterTimer)
                {
                    serverTimer = timers[i];
                    
                }
            }
        }
    }
}
