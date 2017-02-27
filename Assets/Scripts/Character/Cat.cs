using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

abstract public class Cat : Character
{
    public int mana,maxMana;
    public Sprite icon;
    public static bool light;//true = lumiere && false = pas de lumiere
    public List<Spell> spell_list;
    
	public Cat(int x, int y,int z) : base(x, y,z)
	{
		this.width = 1;
		this.height = 1;
		this.depth = 2;
	}
}
