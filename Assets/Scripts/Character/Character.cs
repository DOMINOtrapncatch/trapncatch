﻿using UnityEngine;
using UnityEngine.UI;

abstract public class Character : MonoBehaviour
{
	[Header("Capacity Settings")]

	// Variables qui pourront etres modifiees par l'utilisateur
	[Range(0, 100)]
	public float Attack;
	[Range(0, 100)]
	public float Speed, Defense, MaxLife;

	// Variables qui ne pourront pas etres modifiees par l'utilisateur
	private float Life = 100;

	// Valeur maximales brutes
	private float maxAttackVal = 20, maxSpeedVal = 15, maxDefenseVal = 20, maxLifeVal = 100;

	// Variables utilisees dans les scripts
	public float attack  { get { return Attack  * maxAttackVal  / 100; } set { Attack = value; } }
	public float speed   { get { return Speed   * maxSpeedVal   / 100; } }
	public float defense { get { return Defense * maxDefenseVal / 100; } }
	public float life    { get { return Life    * maxLife       / 100; } set { Life = value; } }
	public float maxLife { get { return MaxLife * maxLifeVal    / 100; } }
}