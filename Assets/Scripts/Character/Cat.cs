﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Net;

public class Cat : Character
{
	// Variables qui pourront etres modifiees par l'utilisateur
	[Range(0, 100)]
	public float Mana;
	[Range(0, 100)]
	public float MaxMana, ManaMaxRecoveryTime;

	// Variables qui ne pourront pas etre modifiees par l'utilisateur
	[HideInInspector]
	public float ManaRecoveryTime = 0;

	// Valeur maximales brutes
	private float maxManaVal = 500, manaMaxRecoveryTimeVal = 40;

	// Variables utilisees dans les scripts
	public float mana                { get { return Mana                * maxMana                / 100; } }
	public float maxMana             { get { return MaxMana             * maxManaVal             / 100; } }
	public float manaMaxRecoveryTime { get { return ManaMaxRecoveryTime * manaMaxRecoveryTimeVal / 100; } }

    public Sprite icon;
    //public static bool light; //true = lumiere && false = pas de lumiere
	public List<Spell> spells;
	[HideInInspector]
	public List<GameObject> nearEnemy = new List<GameObject>();
	[HideInInspector]
	public int enemyKillCount = 0;

	void Update()
	{
		CheckSpells ();
        if (Input.GetKeyDown(KeyCode.P))
            AutoFade.LoadLevel(1, .3f, .3f, Palette.DARK_PURPLE);
    }

	void OnTriggerEnter(Collider col)
	{
		if(col.gameObject.tag == "Enemy")
			nearEnemy.Add(col.gameObject.transform.parent.gameObject);
	}

	void OnTriggerExit(Collider col)
	{
		if(col.gameObject.tag == "Enemy")
			nearEnemy.Remove(col.gameObject.transform.parent.gameObject);
	}

	public void CheckSpells()
	{
		foreach(Spell spell in spells)
		{
			if(spell.CanUse ())
			{
				spell.Activate ();
			}
		}
	}

	public void AttackEnemy(int enemyIndex)
	{
		// Get enemy
		Mouse enemy = nearEnemy[enemyIndex].GetComponent<Mouse>();

		// Remove life
		if(attack > 0)
			enemy.Life -= attack;

		// If dead, make it disappear
		if (enemy.life <= 0)
			KillEnemy(enemyIndex);
	}

	public void KillEnemy(int enemyIndex)
	{
		// Get enemy
		Mouse enemy = nearEnemy[enemyIndex].GetComponent<Mouse>();

		// Spawn particle effect on deth spot and destroy it after it was animated
		GameObject deathParticleInstance = (GameObject)Instantiate(enemy.deathPrefab, nearEnemy[enemyIndex].transform.position, Quaternion.identity);
		Destroy(deathParticleInstance, 1.0f);

		// Destroy corresponding object
		Destroy(nearEnemy[enemyIndex]);

		// Increment number of enemies killes
		++enemyKillCount;
	}
}

