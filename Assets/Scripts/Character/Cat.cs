using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Net;

abstract public class Cat : Character
{
    public float mana, maxMana;
    public Sprite icon;
    //public static bool light; //true = lumiere && false = pas de lumiere
	public List<Spell> spells;
	public List<GameObject> nearEnemy = new List<GameObject>();

	void Update()
	{
		CheckSpells ();
	}

	void OnTriggerEnter(Collider col)
	{
		if(col.gameObject.tag == "Enemy")
			nearEnemy.Add(col.gameObject);
	}
	void OnTriggerExit(Collider col)
	{
		if(col.gameObject.tag == "Enemy")
			nearEnemy.Remove(col.gameObject);
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
}
