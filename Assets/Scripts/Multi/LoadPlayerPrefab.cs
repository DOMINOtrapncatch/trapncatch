﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Networking;

public class LoadPlayerPrefab : NetworkManager
{
    public class MsgTypes
    {
        public const short PlayerPrefab = MsgType.Highest + 1;

        public class PlayerPrefabMsg : MessageBase
        {
            public short controllerID;
            public short prefabIndex;
        }
    }

    public class NetworkSpawn : NetworkManager
    {
        [SerializeField]
        Vector3 playerSpawnPos;
        [SerializeField]
        GameObject classique;
        [SerializeField]
        GameObject puissant;
        [SerializeField]
        GameObject magique;
        [SerializeField]
        GameObject rapide;
        public short playerPrefabIndex;
        private int playerPref;
        private GameObject player;

        public override void OnStartServer()
        {
            NetworkServer.RegisterHandler(MsgTypes.PlayerPrefab, OnResponsePrefab);
            base.OnStartServer();
        }

        public override void OnClientConnect(NetworkConnection conn)
        {
            client.RegisterHandler(MsgTypes.PlayerPrefab, OnRequestPrefab);
            base.OnClientConnect(conn);
        }

        private void OnRequestPrefab(NetworkMessage netMsg)
        {
            MsgTypes.PlayerPrefabMsg msg = new MsgTypes.PlayerPrefabMsg();
            msg.controllerID = netMsg.ReadMessage<MsgTypes.PlayerPrefabMsg>().controllerID;
            msg.prefabIndex = playerPrefabIndex; //add playerprefs
            client.Send(MsgTypes.PlayerPrefab, msg);
        }

        private void OnResponsePrefab(NetworkMessage netMsg)
        {
            MsgTypes.PlayerPrefabMsg msg = netMsg.ReadMessage<MsgTypes.PlayerPrefabMsg>();
            playerPrefab = spawnPrefabs[msg.prefabIndex];
            base.OnServerAddPlayer(netMsg.conn, msg.controllerID);
            Debug.Log(playerPrefab.name + "Spawned !");
        }

        public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
        {
            MsgTypes.PlayerPrefabMsg msg = new MsgTypes.PlayerPrefabMsg();
            msg.controllerID = playerControllerId;
            NetworkServer.SendToClient(conn.connectionId, MsgTypes.PlayerPrefab, msg);
        }

    }

    [SerializeField]
    Vector3 playerSpawnPos;
    [SerializeField]
    GameObject classique;
    [SerializeField]
    GameObject puissant;
    [SerializeField]
    GameObject magique;
    [SerializeField]
    GameObject rapide;

    private GameObject player;

    void Update()
    {
        switch(PlayerPrefs.GetInt("ChoosenCat"))
        {
            case 0:
                player = classique;
                break;
            case 1:
                player = puissant;
                break;
            case 2:
                player = magique;
                break;
            case 3:
                player = rapide;
                break;
            default:
                break;
        }
    }

    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
    {
        var futurplayer = (GameObject)GameObject.Instantiate(player, playerSpawnPos, Quaternion.identity);
        NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
    }

}
