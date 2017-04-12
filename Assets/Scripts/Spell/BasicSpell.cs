using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class BasicSpell : Spell {

    public override void Activate() //attaque basique
    {
        //attaque
		if(cat.nearEnemy.Count > 0)
		{
			Destroy (cat.nearEnemy[0]); // -> THIS IS THE REAL CODE
            
			//AutoFade.LoadLevel(9, .3f, .3f, Color.black);

			// ANMATION

		}
    }
}
