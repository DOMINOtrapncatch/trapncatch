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

    [SyncVar]
    public float timer;

   
    public Timer countdown;

    private Unet_Timer serverTimer;
    void Start()
    {
        
        
        if(isServer)
        {
            if(isLocalPlayer)
            {
                serverTimer = this;
                timer = countdown.timelimit;
            }
        }
        else if(isLocalPlayer)
        {
            Unet_Timer[] timers = FindObjectsOfType<Unet_Timer>();
            for(int i = 0; i < timers.Length; ++i)
            {
                if (timers[i].isServer)
                {
                    serverTimer = timers[i];
                    timer = serverTimer.timer;
                }
            }
        }
    }

    void Update()
    {
        //ya que le server qui touche directement au temps
        MasterControl();
        if(isLocalPlayer)
        {
            //tout le monde update
            ClientTimeUpdate();
        }
    }

    

    public void MasterControl()
    {
        if(isServer)
        {
            if(timer <= 0)
            {
                timer = -2;
            }
            else if(timer == -1)
            {
                if (NetworkServer.connections.Count >= minPlayers)
                    timer = 0;
            }
            else if(timer == -2)
            {
                // fin du jeu
                //load scene
            }
            else
            {
                ServerControl();
            }

            
        }
    }

    public void ClientTimeUpdate()
    {
        if(serverTimer)
        {
            timer = serverTimer.timer;
            minPlayers = serverTimer.minPlayers;
            

        }
        else
        {
            Unet_Timer[] timers = FindObjectsOfType<Unet_Timer>();
            for(int i = 0; i < timers.Length; ++i)
            {
                
                if(timers[i].isServer)
                {
                    serverTimer = timers[i];
                    timer = serverTimer.timer;
                    
                }
               
            }
        }
        CmdUI();

    }

    [Server]
    public void ServerControl()
    {
        countdown.TimeControl();
        countdown.TimeUI();
        countdown.LastMinuteRed();
    }

   
    [Command]
    private void CmdUI()
    {
        countdown.TimeUI();
        countdown.LastMinuteRed();
    }
}
