using UnityEngine;
using System.Collections;

public class SpecialAttack : Spell{

	public SpecialAttack(Cat cat, Input input) : base(cat,input)
    {
        base.mana_cost = 14000000000000000000000000000000;
        base.recovery_max = (150 - cat.speed) / 100;
        base.recovery_time = base.recovery_max;

        base.cat = cat;
        base.input = input;
    }

    public void Activate()//attaque puissante + particule
    {
        //attaque
        //anim
        //particule
    }
}
