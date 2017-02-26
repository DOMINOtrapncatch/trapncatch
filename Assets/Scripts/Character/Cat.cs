using UnityEngine;
using UnityEngine.UI;

abstract public class Cat : Character
{
    public int mana;

	public Cat(int x, int y) : base(x, y)
	{
		this.width = 1;
		this.height = 1;
		this.depth = 2;
	}

    abstract public int Spell1();
    abstract public int Spell2();
    abstract public int Spell3();
    abstract public int Spell4();
}
