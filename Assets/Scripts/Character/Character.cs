using UnityEngine;

abstract public class Character {

	public int x, y;
	public double width, height, depth;
	public int life, maxLife;
	public int attack, speed, defense;

	public Character(int x, int y, int maxLife, int defense)
	{
		this.x = x;
		this.y = y;
		this.life = maxLife;
		this.maxLife = maxLife;
		this.defense = defense;
	}
}