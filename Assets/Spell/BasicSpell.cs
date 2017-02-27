using UnityEngine;
using System.Collections;

public class BasicSpell : Spell {

    
	public BasicSpell (Cat cat, KeyCode input) : base(cat, input)
    {
        base.mana_cost = 0;
        base.recovery_max = (150 - cat.speed) / 100;
        base.recovery_time = base.recovery_max;

        base.cat = cat;
        base.input = input;
    }

    public override void Activate() //attaque basique
    {
        //attaque
		Vector3 pos = transform.position;
		for(int i = 0; i < cat.nearEnemy.Count;i++)
		{
			Vector3 vec = cat.nearEnemy[i].transform.position;
			Vector3 direction = vec - pos;
			if(Vector3.Dot(direction, transform.forward) < 0.7)
			{
				Destroy (cat.nearEnemy[i]);
			}
		}

        //animation + call marc
        
    }
}
