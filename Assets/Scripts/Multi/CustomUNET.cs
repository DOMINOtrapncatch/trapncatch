using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Networking;

public class CustomUNET : NetworkManager
{
    public class MsgTypes
    {
        public const short PlayerPrefab = MsgType.Highest + 1;
        public const short SyncTime = MsgType.Highest + 1;

        public class PlayerPrefabMsg : MessageBase
        {
            public short controllerID;
            public short prefabIndex;
        }
        
    }

    public class SyncTimeMessage : MessageBase
    {
        public double timeStamp;
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

        bool isSyncWithServer = false;

        public override void OnStartServer()
        {
            NetworkServer.RegisterHandler(MsgTypes.PlayerPrefab, OnResponsePrefab);
            base.OnStartServer();

            isSyncWithServer = true;
        }

        public override void OnStartClient(NetworkClient client)
        {
            base.OnStartClient(client);
        }

       
        public override void OnServerConnect(NetworkConnection conn)
        {

            base.OnServerConnect(conn);
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

        void Update()
        {
            
        }

    }

    //player prefab
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
    public MHUDManager myHUD;
    private float timer;

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
        myHUD.player = futurplayer.GetComponent<MCat>();
        NetworkServer.AddPlayerForConnection(conn, futurplayer, playerControllerId);
    }

    //Custom UI 
    #region CUSTOM UI
    public void StartupHost()
    {
        SetPort();
        NetworkManager.singleton.StartHost();
    }

    public void JoinGame()
    {
        SetIPAddress();
        SetPort();
        NetworkManager.singleton.StartClient();
    }

    private void SetIPAddress()
    {
        string ipAddress = GameObject.Find("InputFieldIPAddress").transform.FindChild("Text").GetComponent<Text>().text;
        NetworkManager.singleton.networkAddress = ipAddress;
    }

    private void SetPort()
    {
        NetworkManager.singleton.networkPort = 7777;
    }

    private void OnLevelWasLoaded(int level)
    {
        if(level == 0)
        {
            StartCoroutine(SetupMenuSceneButtons());
        }
        else
        {
            SetupOtherSceneButtons();
        }
    }

    IEnumerator SetupMenuSceneButtons()
    {
        yield return new WaitForSeconds(0.3f);
        GameObject.Find("ButtonStartHost").GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("ButtonStartHost").GetComponent<Button>().onClick.AddListener(StartupHost);

        GameObject.Find("ButtonJoinGame").GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("ButtonJoinGame").GetComponent<Button>().onClick.AddListener(JoinGame);
    }

    private void SetupOtherSceneButtons()
    {
        GameObject.Find("ButtonDisconnect").GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("ButtonDisconnect").GetComponent<Button>().onClick.AddListener(NetworkManager.singleton.StopHost);
    }
    #endregion
}
