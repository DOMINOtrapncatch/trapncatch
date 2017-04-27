using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class NCatAnimController : NetworkBehaviour {

	private Animator anim;

	void Start () {
		anim = gameObject.GetComponent<Animator>();
	}

	void Update (){

        if (!isLocalPlayer)
            return;

		//float move = Input.GetAxis ("Vertical");
		//move *= Input.GetKey (KeyCode.LeftShift) ? 0.4f : 0.2f;
		//anim.SetFloat("Speed", Input.GetKey (KeyCode.Space) ? 1f : move);

		if(Input.GetButton("up") || Input.GetButton("right") || Input.GetButton("down") || Input.GetButton("left"))
		{
			anim.SetInteger("State", (Input.GetButton("attack") || Input.GetButton("attack_special") ? 4 : (Input.GetButton("Jump") ? 3 : (Input.GetButton("run") ? 2 : 1))));
		}
		else if(Input.GetButton("Jump"))
		{
			anim.SetInteger("State", 3);
		}
		else if(Input.GetButton("attack") || Input.GetButton("attack_special"))
		{
			anim.SetInteger("State", 4);
		}
		else
		{
			anim.SetInteger("State", 0);
		}
	}
}
