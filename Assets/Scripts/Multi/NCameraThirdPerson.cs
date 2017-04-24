using UnityEngine;
using System.Collections;

public class NCameraThirdPerson : MonoBehaviour {

    
    public Transform player;
    public float lookSmooth = 0.02f; //how fast we look at out target
    public Vector3 offSetFromTarget = new Vector3(0, 2, -4); // distance camera -> player //ou try 0,-6,-8
    public float camrot_x = 10; //rotation de la camera sur l'axe x

    private Vector3 destination = Vector3.zero; //where we moving to
    private MoveThirdPerson charcontroller;
    private float rotatevelo = 0; //ne pas oublier de le prendre en compte tmtc
    

    void Start()
    {
        SetCameraTarget(player);
    }


    //pour coller la camera a un new player
    //a utiliser pour le swap
    //wonja je t'ai a l'oeil
    void SetCameraTarget(Transform p)
    {
        player = p;

        if (player != null)
        {
			if (player.GetComponent<MoveThirdPerson>())
            {
				charcontroller = player.GetComponent<MoveThirdPerson>();
           }
            else
            {
                Debug.LogError("check presence charac controller + call amandine");
            }

        }
        else
        {
            Debug.LogError("add player 'cause its null/empty + call amandine");
        }
    }

    

    void LateUpdate()
    {
        /*
        mouvement de la camera basé sur mouvement du chara
        donc on utilise un lateupdate vu que les moves de chara se font dans un update et fixedupdate
        */
        
        MoveToTarget();
        LookAtTarget();
    }

    //movement de la camera
    void MoveToTarget()
    {
        destination = charcontroller.get_targetrot * offSetFromTarget;
        destination += player.position;
        transform.position = destination;
       
    }

    void LookAtTarget()//mouvement uniquement sur l'axe y 
    {
        float eulerangle_y = Mathf.SmoothDampAngle(transform.eulerAngles.y,player.eulerAngles.y,ref rotatevelo,lookSmooth);
        transform.rotation = Quaternion.Euler(transform.eulerAngles.x, eulerangle_y, 0);
    }
}
