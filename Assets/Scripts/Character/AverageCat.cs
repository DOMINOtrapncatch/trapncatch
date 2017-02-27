using UnityEngine;
using System.Collections;

public class AverageCat : Cat {

    public AverageCat(int x, int y, int z) : base(x, y,z)
    {
        base.maxLife = 100;
        base.defense = 75;
        base.attack = 75;
        base.speed = 75;
        base.mana = 60;
    }

    
}
