using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class MultipleScratch : Spell
{
    public override void Activate()
    {
        int i = 10;
        while (i > 0)
        {
            cat.spells[0].Activate();
            --i;
        }
    }
}