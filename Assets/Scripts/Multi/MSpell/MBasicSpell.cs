using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Networking;


public class MBasicSpell : MSpell {

    public override void Activate()
    {
		if (cat.nearEnemy.Count > 0)
			cat.AttackEnemy(0);
    }
}
