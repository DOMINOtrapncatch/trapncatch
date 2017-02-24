using System;

public class Mouse : Character
{
	public Mouse(int x, int y, int maxLife, int defense) : base(x, y, maxLife, defense)
	{
		this.width  = .2;
		this.height = .2;
		this.depth  = .2;
	}
}