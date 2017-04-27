using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using System.Text;

//current parsing is string

/*we are using string and not enum or anything else for
  speaking with the server because 
  its easier to use
  and we minimize possible conflict when speaking to arm server
   downside : mistake when writing the string && not pretty

    when adding new function :
    being cautious of using same string between client and server scripts
    
    final touchup :
    (S3) test with stable arm server or webserver
    if enum can go as fast as string
    make things prettier
    try using int and less parsing

 * */

public class SClient 
{
    //client list for server uses only
    //not used in the client ever

    public int s_connectID;
    public string playerName;

    //position update
    public Vector3 pos;
}
public class Server : MonoBehaviour {

    private const int max_connect = 10;

    private int port = 5555; //tweak allowed
    private int hostID;
    private int webhostID;

    private int reliableChannel;
    private int unreliableChannel;

    private bool isStarted = false;

    private byte error;

    private List<SClient> Sclient = new List<SClient>();

    //position update
    private float lastmoveUpdate;
    private float moveUpdateRate = 0.5f; //tweak allowed

    void Start()
    {
        NetworkTransport.Init();

        //set up configs
        ConnectionConfig config = new ConnectionConfig();
        reliableChannel = config.AddChannel(QosType.Reliable);
        unreliableChannel = config.AddChannel(QosType.Unreliable);
        HostTopology topo = new HostTopology(config, max_connect);

        //create server/web socket
        hostID = NetworkTransport.AddHost(topo, port, null);
        webhostID = NetworkTransport.AddWebsocketHost(topo, port, null);

        isStarted = true;
        

    }

    void Update()
    {
        //check if there is a connection
        if (!isStarted)
            return;

        //data receiver
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
            case NetworkEventType.ConnectEvent:
                Debug.Log("Player :" + connectionId + "is connected");
                OnConnection(connectionId);
                break;


            case NetworkEventType.DataEvent:
                string mess = Encoding.Unicode.GetString(recBuffer, 0, dataSize);
                Debug.Log("Receiving from : " + connectionId + "mess : " + mess);
                string[] splitData = mess.Split('|');

                //dem switch listening real hard here
                switch(splitData[0])
                {
                    case "NAMEIS":
                        NameIs(connectionId,splitData[1]);
                        break;

                    case "MYPOS":
                        MyPos(connectionId, float.Parse(splitData[1]), float.Parse(splitData[2]), float.Parse(splitData[3]));
                        break;

                    default:
                        Debug.Log("FATAL ERROR_S : " + mess);
                        break;
                }
                break;

            case NetworkEventType.DisconnectEvent:
                OnDisconnection(connectionId);
                Debug.Log("Player :" + connectionId + "is disconnected");
                break;
        }

        //position update
        if(Time.time - lastmoveUpdate > moveUpdateRate)
        {
            lastmoveUpdate = Time.time;
            string posTemplate = "ASKPOS|";

            //get pos from the world
            foreach(SClient sc in Sclient)
            {
                string sc_x = sc.pos.x.ToString();
                string sc_y = sc.pos.y.ToString(); ;
                string sc_z = sc.pos.z.ToString(); ;

                posTemplate += sc.s_connectID.ToString() + "%" + sc_x + "%" + sc_y + "%" + sc_z + "|";
            }
            posTemplate = posTemplate.Trim();
            Send(posTemplate, unreliableChannel, Sclient);
        }
    }

    #region Fundamental
    void Send(string message, int channelID, int m_connectID)
    {
        
        List<SClient> sc = new List<SClient>();

        //find our client
        sc.Add(Sclient.Find(x => x.s_connectID == m_connectID));

        //sending
        Send(message, channelID, sc);
    }

    void Send(string message, int channelID, List<SClient> Sclient)
    {
        Debug.Log("Sending : " + message);
        byte[] mess = Encoding.Unicode.GetBytes(message);
        int size = 0;

        foreach (SClient sc in Sclient)
        {
            //get accurate size of the message
            size = message.Length * sizeof(char);
            NetworkTransport.Send(hostID, sc.s_connectID, channelID, mess, size, out error);
        }
    }


    #endregion


    #region Listen
    void OnConnection(int connectID)
    {
        //add new client to the list
        SClient sc = new SClient();

        //tell the server -> clientID
        sc.s_connectID = connectID;
        sc.playerName = "TMP";
        Sclient.Add(sc);

        //get client name and sent it to other player
        string nameTemplate = "ASKNAME|" + connectID + "|";
        foreach(SClient sc_ in Sclient)
        {
            nameTemplate += sc_.playerName + "%" + sc.s_connectID + "|";
        }
        nameTemplate = nameTemplate.Trim('|');

        Send(nameTemplate, reliableChannel, connectID);
    }

    void OnDisconnection(int m_connectID)
    {
        //remove
        Sclient.Remove(Sclient.Find(x => x.s_connectID == m_connectID));

        //tell the world
        string decoTemplate = "DC|" + m_connectID;
        Send(decoTemplate, reliableChannel, Sclient);
    }
    void NameIs(int m_connectID, string playerName)
    {
        //link name to the connectID
        Sclient.Find(x => x.s_connectID == m_connectID).playerName = playerName;

        //tell the world
        string connectTemplate = "CO|" + playerName + "|" + m_connectID;
        Send(connectTemplate,reliableChannel,Sclient);
    }

    void MyPos(int m_connectID, float x, float y, float z)
    {
        Sclient.Find(x_ => x_.s_connectID == m_connectID).pos = new Vector3(x, y, z);
    }

    #endregion


}
