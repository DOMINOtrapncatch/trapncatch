﻿﻿using System;
using UnityEngine;

abstract public class Character : MonoBehaviour
{
	[Header("Capacity Settings")]

	// Variables qui pourront etres modifiees par l'utilisateur
	[Range(0, 100)]
	public float Attack;
	[Range(0, 100)]
	public float Speed, Defense, MaxLife;

	// Variables qui ne pourront pas etres modifiees par l'utilisateur
	[HideInInspector]
	public float Life = 100;

	// Valeur maximales brutes
	private float maxAttackVal = 20, maxSpeedVal = 12, maxDefenseVal = 20, maxLifeVal = 100;

	// Variables utilisees dans les scripts
	public float attack  { get { return Attack  * maxAttackVal  / 100; } }
	public float speed   { get { return Speed   * maxSpeedVal   / 100; } }
	public float defense { get { return Defense * maxDefenseVal / 100; } }
	public float life    { get { return Life    * maxLife       / 100; } }
	public float maxLife { get { return MaxLife * maxLifeVal    / 100; } }

	/*
	 * @return Still alive ?
	 */
	public virtual bool Damage(float damage)
	{
		float newLife = Life - damage;

		if (newLife > 0)
			Life = newLife;

		return newLife > 0;
	}

	public void Heal(int heal)
	{
		float newLife = Life + heal;

		if (newLife > maxLife)
			Life = maxLife;
	}

	public void Destroy()
	{
		Destroy(this.gameObject);
	}
}