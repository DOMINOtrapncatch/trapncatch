using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Net;

abstract public class Cat : Character
{
    public int mana,maxMana;
    public Sprite icon;
    public static bool light;//true = lumiere && false = pas de lumiere
	public List<Spell> spell_list;

	public List<GameObject> nearEnemy = new List<GameObject>();
    
	public Cat(int x, int y,int z) : base(x, y,z)
	{
		this.width = 1;
		this.height = 1;
		this.depth = 2;
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

	public void Check()
	{
		foreach(Spell spell in spell_list)
		{
			if(spell.CanUse ())
			{
				spell.Activate ();
			}
		}
	}
}
