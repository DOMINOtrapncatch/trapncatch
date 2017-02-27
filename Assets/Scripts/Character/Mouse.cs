using System;

public class Mouse : Character
{
	public Mouse(int x, int y,int z)
	{
		base.Init (x, y, z);

		this.width  = .2;
		this.height = .2;
		this.depth  = .2;
	}
}