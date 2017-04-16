using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Types;
using UnityEngine.Networking.Match;
using System.Collections;
using System.Collections.Generic;

public class Matching : NetworkBehaviour {

    //matchmaker
    List<MatchInfoSnapshot> matchlist = new List<MatchInfoSnapshot>();
    bool matchlist_created;
    bool matchlist_joined;
    MatchInfo matchinfo;
    string matchname = "New Garden"; //modif
    NetworkMatch netmatch;

    //connection
    int hostid = -1;
    List<int> connectIDs = new List<int>(); //server has multiple connection
    byte[] receive_buff;
    string netmess = "Hello DOMINO";
    string lastmess = "";
    NetworkWriter writer;
    NetworkReader reader;
    bool connectok;

    const int serverport = 25000;
    const int maxsizemess = 65535;

    void Awake()
    {
        netmatch = gameObject.AddComponent<NetworkMatch>();
    }

    void Start()
    {
        receive_buff = new byte[maxsizemess];
        writer = new NetworkWriter();

        //if localhost -> en gros il va search pour les autres joueurs si on reste que sur une machine ok
        Application.runInBackground = true;
    }

    void OnApplicationQuit()
    {
        NetworkTransport.Shutdown();
    }

    void OnGUI() //customisable tacru
    {
        if(string.IsNullOrEmpty(Application.cloudProjectId))
        {
            GUILayout.Label("Set up the project first");
        }
        else
        {
            GUILayout.Label("Ok :" + Application.cloudProjectId);

        }

        if(matchlist_joined)
        {
            GUILayout.Label(matchname + "is here");

        }
        else if(matchlist_created)
        {
            GUILayout.Label(matchname + "created");
        }

        GUILayout.Label("Connection : " + connectok);

        if (matchlist_created || matchlist_joined)
        {
            GUILayout.Label("Address :" + matchinfo.address + " and Port :" + matchinfo.port);//info server
            GUILayout.Label("MatchID :" + matchinfo.networkId + " clientId :" + matchinfo.nodeId);//info matchmaker && client



            #region message manager GUI
            GUILayout.BeginHorizontal();
            GUILayout.Label("Out message");
            netmess = GUILayout.TextField(netmess);
            GUILayout.EndHorizontal();
            GUILayout.Label("Last message : " + lastmess);

            if (connectok && GUILayout.Button("Send message"))
            {
                writer.SeekZero();
                writer.Write(netmess);
                byte error;
                //envoie du message a tous les clients
                for (int i = 0; i < connectIDs.Count; ++i)
                {
                    NetworkTransport.Send(hostid, connectIDs[i], 0, writer.AsArray(), writer.Position, out error);
                    if ((NetworkError)error != NetworkError.Ok)
                    {
                        Debug.Log("Failed in sending message : " + (NetworkError)error);
                    }
                }
            }
            #endregion

            //shutting down
            if (GUILayout.Button("Exit"))
            {
                netmatch.DropConnection(matchinfo.networkId, matchinfo.nodeId, 0, OnConnectionDropped);
            }

        }
         //..... on a besoin que d'un room nous en soit donc on s'arrete la
        foreach(var match in matchlist)
        {
            if(GUILayout.Button(match.name))
            {
                //netmatch.JoinMatch(match.networkId, "", "", "", 0, 0, OnMatchJoined);
            }
        }
    }

    public void OnConnectionDropped(bool success,string extendedInfo)
    {
        Debug.Log("Connection has been dropped");
        NetworkTransport.Shutdown();
        hostid = -1;
        connectIDs.Clear();
        matchinfo = null;
        matchlist_created = false;
        matchlist_joined = false;
        connectok = false;
    }

    public virtual void OnMatchCreate(bool success, string extendedInfo,MatchInfo matchinfo2)
    {
        if(success)
        {
            Debug.Log("Create match success");
            Utility.SetAccessTokenForNetwork(matchinfo.networkId, matchinfo.accessToken);

            matchlist_created = true;
            matchinfo = matchinfo2;

            //OnStartServer(matchinfo.address, matchinfo.port, matchinfo.networkId);
            
        }
        else
        {
            Debug.LogError("Create match failed : " + extendedInfo);
        }
    }



}
