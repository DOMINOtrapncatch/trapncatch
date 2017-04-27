using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Text;

public class Player
{
    public string playerName;
    public GameObject gameobj;
    public int connectID;
}
public class Client : MonoBehaviour {

    private const int max_connect = 10;

    private int port = 5555; //tweak allowed
    private int hostID;
    private int connectID;
    private int myclientID;

    private int reliableChannel;
    private int unreliableChannel;
    

    private float connectTime;

    private bool isStarted = false;
    private bool isConnected = false;

    private string playerName;

    private byte error;

    public GameObject playerPrefab;
    public Dictionary<int,Player> player_rep = new Dictionary<int, Player>();

    public void Connect()
    {
        //get player name, if nothing -> no player -> no connecting
        string tempName = GameObject.Find("Login").GetComponent<InputField>().text;
        if(tempName == "")
        {
            Debug.Log("Amandine - InputField_Multi");
            return;
        }
        playerName = tempName;
        
        //usual connection can begin
        NetworkTransport.Init();

        //set up configs
        ConnectionConfig config = new ConnectionConfig();
        reliableChannel = config.AddChannel(QosType.Reliable);
        unreliableChannel = config.AddChannel(QosType.Unreliable);
        HostTopology topo = new HostTopology(config, max_connect);

        //create server/client socket
        hostID = NetworkTransport.AddHost(topo,0);
        connectID = NetworkTransport.Connect(hostID, "127.0.0.1", port, 0, out error);

        isConnected = true;
        connectTime = Time.time;
        
    }

    void Update()
    {
        //check if there is a connection
        if (!isConnected)
            return;

        //data sender
        int recHostId;
        int connectionId;
        int channelId;
        byte[] recBuffer = new byte[1024];
        int bufferSize = 1024;
        int dataSize;
        byte error;
        NetworkEventType recData = NetworkTransport.Receive(out recHostId, out connectionId, out channelId, recBuffer, bufferSize, out dataSize, out error);
        switch (recData)
        {
        
            case NetworkEventType.DataEvent:
                string mess = Encoding.Unicode.GetString(recBuffer, 0, dataSize);
                Debug.Log("Receive :" + mess);
                string[] splitData = mess.Split('|');

                //demmm switchh speaking so loud
                switch(splitData[0])
                {
                    case "ASKNAME":
                        AskName(splitData);
                        break;

                    case "CO":
                        SpawnPlayer(splitData[1],int.Parse(splitData[2]));
                        break;

                    case "DC":
                        PlayerDisconnected(int.Parse(splitData[1]));
                        break;

                    case "ASKPOS":
                        AskPos(splitData);
                        break;

                    default:
                        Debug.Log("FATAL ERROR_C : " + mess);
                        break;
                }

                break;
        }
    }


    #region Fundamental
    void Send(string message, int channelID)
    {
        Debug.Log("Sending : " + message);
        byte[] mess = Encoding.Unicode.GetBytes(message);
        int size = message.Length * sizeof(char);
        NetworkTransport.Send(hostID, connectID, channelID, mess, size, out error);
    }
    #endregion

    #region Speaking
    void AskName (string[] data)
    {
        //create client and send name to server
        myclientID = int.Parse(data[1]);
        string nameTemplate = "NAMEIS|" + playerName;
        Send(nameTemplate, reliableChannel);
        //create the world
        for(int i = 2; i < data.Length-1;++i)
        {
            string[] tempData = data[i].Split('%');
            SpawnPlayer(tempData[0], int.Parse(data[1]));
        }
    }

    void AskPos(string[] data)
    {
        if (!isStarted)
            return;

        //update the world
        for(int i = 1; i < data.Length - 1; ++i)
        {
            string[] tempData = data[i].Split('%');

            //ok alors le server tu te calmes
            // tu updates les autres pas moi 
            if (myclientID != int.Parse(tempData[0]))
            {
                Vector3 pos = Vector3.zero;
                pos.x = float.Parse(tempData[1]);
                pos.y = float.Parse(tempData[2]);
                pos.z = float.Parse(tempData[3]);
                player_rep[int.Parse(tempData[0])].gameobj.transform.position = pos;
            }
            
        }

        //send my position
        Vector3 mypos = player_rep[myclientID].gameobj.transform.position;
        string posTemplate2 = "MYPOS|" + mypos.x.ToString() + "|" + mypos.y.ToString() + "|" + mypos.z.ToString();
        Send(posTemplate2, unreliableChannel);
    }

    void SpawnPlayer(string playerName,int m_connectID)
    {
        GameObject go = Instantiate(playerPrefab) as GameObject;

        //if its current player
        if(m_connectID == myclientID)
        {
            //adding component
            go.AddComponent<MoveThirdPerson>();
            GameObject.Find("StartConnecting").SetActive(false);
            isStarted = true;
        }

        //sinon bah on en crée un kestuvafaire
        Player newplayer = new Player();
        newplayer.gameobj = go;
        newplayer.playerName = playerName;
        newplayer.connectID = m_connectID;

        //maj le nametag 
        newplayer.gameobj.GetComponentInChildren<TextMesh>().text = playerName;
        //casually keeping tracks
        player_rep.Add(m_connectID,newplayer); 

    }

    void PlayerDisconnected(int m_connectID)
    {
        Destroy(player_rep[m_connectID].gameobj);
        player_rep.Remove(m_connectID);
    }

    #endregion





}
