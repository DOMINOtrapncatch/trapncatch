using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class BasicSpell : Spell {

    public override void Activate()
    {
		if(cat.nearEnemy.Count > 0)
		{
			// Get enemy
			Mouse enemy = cat.nearEnemy[0].GetComponent<Mouse>();

			// Remove life
			if(cat.attack > 0)
				enemy.life -= cat.attack;

			// If dead, make it disappear
			if(enemy.life <= 0)
                Destroy(cat.nearEnemy[0]);
		}
    }
}
