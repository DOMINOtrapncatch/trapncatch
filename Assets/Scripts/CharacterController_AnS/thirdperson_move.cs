using UnityEngine;
using System.Collections;

public class thirdperson_move : MonoBehaviour {

    //ON OUBLIT PAS LE RIGIDBODY SVP

    public float inputdelay = 0.1f; //on peut enlever yolo
    public float forwardvelo = 12;
    public float rotatevelo = 100;

    private Quaternion targetrot;
    private Rigidbody rigbody;

    private float forwardinput;
    private float turninput;
    
    //getter pour targetrot
    //je le laisse au cas ou mais au final je l'utilise pas
    public Quaternion get_targetrot
    {
        get { return targetrot; }
    }

    private void Start()
    {
        targetrot = transform.rotation;

        if (GetComponent<Rigidbody>())
        {
            rigbody = GetComponent<Rigidbody>();
        }
        else
        {
            Debug.LogError("rigidbody inexistant + Call Amandine");
        }
        forwardinput = 0;
        turninput = 0;
    }

    private void Update()
    {
        GetInput();
        Turn();
    }

    private void FixedUpdate() //manage moves that required physics (jump,run)
    {
        Run();
    }

    private void GetInput()
    {
        forwardinput = Input.GetAxis("Vertical");//pour vertical/hori check unity properties
        // -1 < forwardinput < 1

        turninput = Input.GetAxis("Horizontal");
    }


    //moves
    private void Run()
    {
        //deadzone = zone de delai
        //gestion deadzone
        if (Mathf.Abs(forwardinput) > inputdelay)
        {
            //on fait bouger le charac
            rigbody.velocity = transform.forward * forwardinput * forwardvelo;
            /*
                forwardinput peut etre soit >0 soit <0, c'est ce qui gere si on avance ou on recule            
            */
        }
        else
        {
            //sinon le charact bouge pas
            rigbody.velocity = Vector3.zero;
        }
    }

    private void Turn()
    {
        //gestiond deadzone
        if (Mathf.Abs(turninput) > inputdelay)
        {
            targetrot *= Quaternion.AngleAxis(rotatevelo * turninput * Time.deltaTime, Vector3.up);
            //meme delire que run() -> cest le signe de turninput qui gere si on tourne a gauche ou a droite
        }
        transform.rotation = targetrot;
    }
}
