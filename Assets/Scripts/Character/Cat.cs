using UnityEngine;

public class Cat : Character {

	int magic;

	public Cat(int x, int y, int maxLife, int defense) : base(x, y, maxLife, defense)
	{
		this.width = 1;
		this.height = 1;
		this.depth = 2;
	}

}
