using UnityEngine;
using System.Collections;

public class BasicSpell : Spell {

	public BasicSpell (Cat cat, KeyCode input) : base (cat, input)
    {
        mana_cost = 40;
		recovery_max = 100;//(150 - cat.speed) / 100;
        recovery_time = recovery_max;
    }

    public override void Activate() //attaque basique
    {
        //attaque
		if(cat.nearEnemy.Count > 0)
			Destroy (cat.nearEnemy[0]);

        //animation + call marc
        
    }
}
