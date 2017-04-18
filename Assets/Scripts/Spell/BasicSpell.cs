using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class BasicSpell : Spell {

    public override void Activate()
    {
		if (cat.nearEnemy.Count > 0)
			cat.AttackEnemy(0);
    }
}
