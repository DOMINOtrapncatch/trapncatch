using UnityEngine;
using System.Collections;

public class BasicSpell : Spell {

    
	public BasicSpell (Cat cat, Input input) : base(cat,input)
    {
        base.mana_cost = 0;
        base.recovery_max = (150 - cat.speed) / 100;
        base.recovery_time = base.recovery_max;

        base.cat = cat;
        base.input = input;
    }

    public void Activate() //attaque basique
    {
        //attaque
        

        //animation + call marc
        
        
    }
}
