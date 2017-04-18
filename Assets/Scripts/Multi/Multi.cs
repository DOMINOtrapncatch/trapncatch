using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class Multi : NetworkBehaviour {

    /*
     1) opening socket
     2) connect to another socket
     3) send messages
     4) receive messages

     */
    public uint listen_mess = 8;
    byte channelID;

    int socketID_server = -1;
    int socketID_client = -1;
    int localport = 8888;
    int socketPort = 7777; // NE PAS OUBLIER DE MODIF CECI
    int connectID;
    int maxConnect = 8;

    string host = "127.0.0.1"; //idem

    bool host_init = false;
    bool client_connected = false;
    void Start () {
        //global config
        GlobalConfig globalconfig = new GlobalConfig();
        globalconfig.ReactorModel = ReactorModel.FixRateReactor;
        globalconfig.ThreadAwakeTimeout = listen_mess;

        //setup
        //init channel for futur send/receive actions
        //set up max numbers of connection for the socket tmtc
        ConnectionConfig config = new ConnectionConfig();
        channelID = config.AddChannel(QosType.Reliable); //less faster but safe
        HostTopology topo = new HostTopology(config, maxConnect);

        //init socket server and client
        socketID_server = NetworkTransport.AddHost(topo, localport);
        socketID_client = NetworkTransport.AddHost(topo);
        Debug.Log("Server : " + socketID_server);
        Debug.Log("Client : " + socketID_client);

        host_init = true;
        Connect();
        
	}
	
	public void Connect()
    {
        byte error;

        //NE PAS OUBLIER DE MODIF
        connectID = NetworkTransport.Connect(socketID_client, host, socketPort, 0, out error);
        NetworkError neterror = (NetworkError)error;
        if(neterror != NetworkError.Ok)
        {
            Debug.Log("Nope");
        }
        else
        {
            Debug.Log("Connected to Server :" + connectID);
        }
        
        
    }

    public void Disconnect()
    {
        byte error;
        NetworkTransport.Disconnect(socketID_client, connectID, out error);
    }

    public void SendSocketMessage()
    {
        byte error;
        byte[] buffer = new byte[1024];
        Stream stream = new MemoryStream(buffer);
        BinaryFormatter format = new BinaryFormatter();
        format.Serialize(stream, "HelloDOMINO");

        NetworkTransport.Send(socketID_client, connectID, channelID, buffer, 1024, out error);


    }


    void Update()
    {
        //Listening/receive message

        int hostid_rec;
        int connectID_rec;
        int channelID_rec;
        byte[] buffer_rec = new byte[1024];
        int dataSize;
        byte error;

        NetworkEventType netEvent = NetworkTransport.Receive(out hostid_rec, out connectID_rec, out channelID_rec, buffer_rec, 1024, out dataSize, out error);

        /*
         Enum with 4 types
         1) Nothing
         2) ConnectionEvent -> socket up
         3) DataEvent -> message received
         4) DisconnectEvent
         */

        switch(netEvent)
        {
            case NetworkEventType.Nothing:
                break;

            case NetworkEventType.ConnectEvent:
                
                Debug.Log("New connection received");
                //ADD player prefab, display text etc etc
                
                break;

            case NetworkEventType.DataEvent:

                Stream stream = new MemoryStream(buffer_rec);
                BinaryFormatter format = new BinaryFormatter();
                string message = format.Deserialize(stream) as string;
                Debug.Log("New message received");

                break;

            case NetworkEventType.DisconnectEvent:

                Debug.Log("Remote client disconnected");
                //destroy player etc 

                break;

        }
    }

}
