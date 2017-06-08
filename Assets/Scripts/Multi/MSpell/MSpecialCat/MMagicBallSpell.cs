using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MMagicBallSpell : MSpell
{
	public GameObject magicBallParticule;
	public GameObject magicBallExplodeParticule;

	public float damage = 30;
	public float magicBallSpeed = 200;

	public override void Activate()
	{
		StartCoroutine("SendProjectile");
	}

	IEnumerator SendProjectile()
	{
		GameObject magicBall = (GameObject)Instantiate(magicBallParticule, cat.transform.position + new Vector3(0, 0.2f, 0), cat.transform.rotation);
        NetworkServer.Spawn(magicBall);

		magicBall.GetComponent<Rigidbody>().AddForce(cat.transform.forward * magicBallSpeed);

		ParticleSystem magicBallParticle = magicBall.GetComponent<ParticleSystem>();
		magicBallParticle.Play();

		SphereCollider magicBallCollider = magicBall.GetComponent<SphereCollider>();

		float startTime = Time.fixedTime;
		List<Collider> enemies = new List<Collider>();

		while (enemies.Count == 0 && Time.fixedTime - startTime < 2.5f)
		{
			Collider[] colliders = Physics.OverlapSphere(magicBallCollider.transform.position, magicBallCollider.radius);

			foreach (Collider col in colliders)
				if (col.tag == "Enemy")
					enemies.Add(col);

			if(enemies.Count > 0)
			{
				magicBallParticle.Stop();
				Destroy(magicBallParticle.gameObject);

				GameObject magicBallExplode = (GameObject)Instantiate(magicBallExplodeParticule, magicBallParticle.transform.position, magicBallParticle.transform.rotation);
                NetworkServer.Spawn(magicBallExplode);
				magicBallExplode.GetComponent<ParticleSystem>().Play();
				Destroy(magicBallExplode, 1.0f);

				foreach(Collider enemy in enemies)
				{
					MCharacter charact = enemy.gameObject.GetComponentInParent<MCharacter>();
					if (!charact.Damage(damage))
					{
						charact.Destroy();
						cat.enemyKillCount++;
					}
				}
			}

			yield return new WaitForSeconds(0.2f);
		}

		if(enemies.Count <= 0)
		{
			magicBallParticle.Stop();
            Destroy(magicBallParticle.gameObject);
		}		
	}
}
