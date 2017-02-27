using UnityEngine;
using System.Collections;
using System;

public class BlackCat : Cat
{
    public BlackCat(int x, int y) : base(x, y)
    {
        base.maxLife = 100;
        base.defense = 55;
        base.attack = 65;//t'avais mis 60 mais c'est 65 >_<
        base.speed = 100;
        base.mana = 50;
    }

    public override int Spell1()
    {
        return 0;
    }

    public override int Spell2()
    {
        return 0;
    }

    public override int Spell3()
    {
        return 0;
    }

    public override int Spell4()
    {
        return 0;
    }
}
