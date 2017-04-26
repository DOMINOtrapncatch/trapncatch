using System;
using System.Collections;
using UnityEngine;

public class MagicBallSpell : Spell
{
	public GameObject magicBallParticule;

	public float magicBallSpeed = 200f;

	public override void Activate()
	{
		StartCoroutine("SendProjectile");
	}

	void SendProjectile()
	{
		GameObject magicBall = (GameObject)Instantiate(magicBallParticule, cat.transform.position + new Vector3(0, 0.2f, 0), cat.transform.rotation);

		magicBall.GetComponent<Rigidbody>().AddForce(cat.transform.forward * magicBallSpeed);

		// TODO - 1. Check for collision
		//        2. Stop ball animation
		//        3. Start explosion animtion
		//        4. Handle surrounding damage
		//        5. (OPTION) Handle surrounding velocity
	}
}
