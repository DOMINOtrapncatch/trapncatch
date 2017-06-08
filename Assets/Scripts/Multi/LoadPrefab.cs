using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;

public class LoadPrefab : NetworkLobbyManager {
    /*
    public Object[] cat_prefabs;
    public NetworkManager netMan;
    private int prefab = 1;
    private GameObject player;
    // Use this for initialization
    void Start()
    {
        //get player's cat
        //prefab = PlayerPrefs.GetInt("ChoosenCat");

        //override network manager spawner
        //Destroy(netMan.playerPrefab);
        netMan.playerPrefab = (GameObject)cat_prefabs[prefab];
        player = netMan.playerPrefab;
        print(player.name);
        //ClientScene.RegisterPrefab(player);
        //spawn
        if (cat_prefabs != null && prefab >= 0)
        {
            player = Instantiate(cat_prefabs[prefab], transform.position, Quaternion.identity) as GameObject;
            
        }   
    }
    */
    private Dictionary<int, int> currentPlayer;
    
    public override GameObject OnLobbyServerCreateLobbyPlayer(NetworkConnection conn, short playerControllerId)
    {
        if (!currentPlayer.ContainsKey(conn.connectionId))
            currentPlayer.Add(conn.connectionId, 0);
        return base.OnLobbyServerCreateLobbyPlayer(conn, playerControllerId);
    }

    public void SetPlayerCatLobby(NetworkConnection conn)
    {
        if (currentPlayer.ContainsKey(conn.connectionId))
            currentPlayer[conn.connectionId] = PlayerPrefs.GetInt("ChoosenCat");
    }
    public override GameObject OnLobbyServerCreateGamePlayer(NetworkConnection conn, short playerControllerId)
    {
        int index = currentPlayer[conn.connectionId];
        GameObject player = (GameObject) GameObject.Instantiate(spawnPrefabs[index], startPositions[conn.connectionId].position,Quaternion.identity);
        NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
        return player;
    }

}
