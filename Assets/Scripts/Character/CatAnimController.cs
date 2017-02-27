using UnityEngine;
using System.Collections;

public class CatAnimController : MonoBehaviour {

	private Animator anim;

	void Start () {
		anim = gameObject.GetComponentInChildren<Animator>();
	}

	void Update (){
		float move = Input.GetAxis ("Vertical");
		move *= Input.GetKey (KeyCode.LeftShift) ? 1.0f : 0.3f;
		anim.SetFloat("Speed", move);
	}
}
