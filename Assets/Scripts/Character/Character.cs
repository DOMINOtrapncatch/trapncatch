using UnityEngine;

abstract public class Character
{
	public int x, y;
	public double width, height, depth;
	public int life, maxLife;
	public int attack, speed, defense;

	public Character(int x, int y)
	{
		this.x = x;
		this.y = y;
		this.life = maxLife;
	}
}