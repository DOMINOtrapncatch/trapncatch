using UnityEngine;
using System.Collections;
using System;

public class BlackCat : Cat
{
    
    //spell1
    //spell2
    //spell3
    //spell4
    public BlackCat(int x, int y, int z) : base(x, y, z)
    {
        base.maxLife = 100;
        base.defense = 55;
        base.attack = 65;//t'avais mis 60 mais c'est 65 >_<
        base.speed = 100;
        base.mana = 50;
        base.spell_list.Add(spell1);
    }

    
}
