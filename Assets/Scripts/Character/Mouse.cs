using System;

public class Mouse : Character
{
	public Mouse(int x, int y) : base(x, y)
	{
		this.width  = .2;
		this.height = .2;
		this.depth  = .2;
	}
}