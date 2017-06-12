using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

abstract public class MCharacter : NetworkBehaviour
{
	[Header("Capacity Settings")]

	// Variables qui pourront etres modifiees par l'utilisateur
	[Range(0, 100)]
	public float Attack;
	[Range(0, 100)]
	public float Speed, Defense, MaxLife;

	// Variables qui ne pourront pas etres modifiees par l'utilisateur
	[HideInInspector]
	[SyncVar]public float Life = 100;

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
	public bool Damage(float damage)
	{
		float newLife = Life - damage;

		if (newLife > 0)
			Life = newLife;

		return newLife > 0;
	}

	public void Destroy()
	{
		Destroy(this.gameObject);
	}
}