using UnityEngine;
using System.Collections;

public class BasicSpell : Spell {

    public override void Activate() //attaque basique
    {
        //attaque
		if(cat.nearEnemy.Count > 0)
			Destroy (cat.nearEnemy[0]);

        //animation + call marc
        
    }
}
