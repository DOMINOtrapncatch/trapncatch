using UnityEngine;
using System.Collections;

public class SpecialAttack : Spell
{
	public SpecialAttack(Cat cat, KeyCode input) : base(cat, input)
    {
        base.mana_cost = 14;
        base.recovery_max = (150 - cat.speed) / 100;
        base.recovery_time = base.recovery_max;

        base.cat = cat;
        base.input = input;
    }

    public override void Activate()//attaque puissante + particule
    {
        //attaque
        //anim
        //particule
    }
}
