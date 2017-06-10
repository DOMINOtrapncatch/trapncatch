using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Unet_Timer : NetworkBehaviour {

    [SyncVar]
    public float timerLimit;
    public float currentTimer;
    //-1 = attente de joueurs
    //-2 = fin du jeu
    public int minPlayers;
    public bool masterTimer = false;

    private Unet_Timer serverTimer;

    void Start()
    {
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
            Unet_Timer[] timers = FindObjectsOfType<Unet_Timer>();
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
        if(masterTimer)
        {
            if(currentTimer >= timerLimit)
            {
                currentTimer = -2;
            }
            else if(currentTimer == -1)
            {
                if (NetworkServer.connections.Count >= minPlayers)
                    currentTimer = 0;
            }
            else if(currentTimer == -2)
            {
                // fin du jeu
            }
            else
            {
                currentTimer += Time.deltaTime;
            }
        }
    }

    public void ClientTimeUpdate()
    {
        if(serverTimer)
        {
            timerLimit = serverTimer.timerLimit;
            currentTimer = serverTimer.currentTimer;
            minPlayers = serverTimer.minPlayers;
        }
        else
        {
            Unet_Timer[] timers = FindObjectsOfType<Unet_Timer>();
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
