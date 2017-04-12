using UnityEngine;
using System.Collections;

public class CatAnimController : MonoBehaviour {

	private Animator anim;

	void Start () {
		anim = gameObject.GetComponentInChildren<Animator>();
	}

	void Update (){
		float move = Input.GetAxis ("Vertical");
		move *= Input.GetKey (KeyCode.LeftShift) ? 0.4f : 0.2f;
		anim.SetFloat("Speed", Input.GetKey (KeyCode.Space) ? 1f : move);
	}
}
