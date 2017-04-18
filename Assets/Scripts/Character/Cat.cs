using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Net;

abstract public class Cat : Character
{
	// Variables qui pourront etres modifiees par l'utilisateur
	[Range(0, 100)]
	public float Mana;
	[Range(0, 100)]
	public float MaxMana;

	// Valeur maximales brutes
	private float maxManaVal = 100;

	// Variables utilisees dans les scripts
	public float mana    { get { return Mana    * maxMana    / 100; } set { Mana = value; } }
	public float maxMana { get { return MaxMana * maxManaVal / 100; } }

    public Sprite icon;
    //public static bool light; //true = lumiere && false = pas de lumiere
	public List<Spell> spells;
	[HideInInspector]
	public List<GameObject> nearEnemy = new List<GameObject>();
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
			enemy.life -= attack;

		// If dead, make it disappear
		if (enemy.life <= 0)
			KillEnemy(enemyIndex);
	}

	public void KillEnemy(int enemyIndex)
	{
        Destroy(nearEnemy[enemyIndex]);
		++enemyKillCount;
	}
}

