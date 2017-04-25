using UnityEngine;
using System.Collections;

public class MoveThirdPerson : MonoBehaviour {
    //ON OUBLIT PAS LE RIGIDBODY SVP freez rotation x,y,z + isgravity=false
    //add collider
    //isground = everything


    //add class for jump
    [System.Serializable]
    public class MoveSettings
    {
        public Character chara_cat;
        public float forwardvelo = 12;
        //public float forwardvelo = Character
        public float rotatevelo = 100;
        public float jumpvelo = 13;
        public float dist_to_ground = 0.1f; //if we're in the air or not
        public LayerMask ground;
    }
    [System.Serializable]
    public class PhysSettings
    {
        public float downaccel = 0.75f;
    }
    [System.Serializable]
    public class InputSettings
    {
      public float inputdelay = 0.1f; //on peut enlever yolo
        public string forward_axis = "Vertical";
        public string turn_axis = "Horizontal";
        public string jump_axis = "Jump";
        
    }

    public MoveSettings movesettings = new MoveSettings();
    public PhysSettings physsettings = new PhysSettings();
    public InputSettings inputsettings = new InputSettings();

    private Vector3 velocity = Vector3.zero;
    private Quaternion targetrot;
    private Rigidbody rigbody;

    private float forwardinput;
    private float turninput;
    private float jumpinput;
    
    //getter pour targetrot
    //je le laisse au cas ou mais au final je l'utilise pas
    public Quaternion get_targetrot
    {
        get { return targetrot; }
    }

    //jump
    bool Grounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, movesettings.dist_to_ground, movesettings.ground);
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
        jumpinput = 0;
    }
    
    
    private void Update()
    {
        GetInput();
        Turn();
    }

    private void FixedUpdate() //manage moves that required physics (jump,run)
    {

        
        Run();
        Jump();

        rigbody.velocity = transform.TransformDirection(velocity);
        //rigbody.velocity = velocity;
    }

    private void GetInput()//optimisé
    {
        forwardinput = Input.GetAxis(inputsettings.forward_axis);//pour vertical/hori check unity properties
        // -1 < forwardinput < 1

        turninput = Input.GetAxis(inputsettings.turn_axis);
        jumpinput = Input.GetAxisRaw(inputsettings.jump_axis);//not interpolated -> either -1 or 1

    }


    //moves
    private void Run()
    {
        //deadzone = zone de delai
        //gestion deadzone
        if (Mathf.Abs(forwardinput) > inputsettings.inputdelay)
        {
            //on fait bouger le charac
            rigbody.velocity = transform.forward * forwardinput * movesettings.forwardvelo;
            /*
                forwardinput peut etre soit >0 soit <0, c'est ce qui gere si on avance ou on recule            
            */

            velocity.z = movesettings.forwardvelo * forwardinput;
            //Debug.LogError(velocity.z);
        }
        else
        {
            //sinon le charact bouge pas
            //rigbody.velocity = Vector3.zero;
            velocity.z = 0;
        }
    }

    private void Turn()
    {
        //gestiond deadzone
        if (Mathf.Abs(turninput) > inputsettings.inputdelay)
        {
            targetrot *= Quaternion.AngleAxis(movesettings.rotatevelo * turninput * Time.deltaTime, Vector3.up);
            //meme delire que run() -> cest le signe de turninput qui gere si on tourne a gauche ou a droite
        }
        transform.rotation = targetrot;
    }

    private void Jump()
    {
        if(jumpinput > 0 && Grounded())//jump
        {
            velocity.y = movesettings.jumpvelo;
        }
        else if(jumpinput == 0 && Grounded())//rien
        {
            velocity.y = 0;
        }
        else//falling
        {
            velocity.y -= physsettings.downaccel;
        }
    }
}
