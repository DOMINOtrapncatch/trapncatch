using UnityEngine;
using System.Collections;
using System.Runtime.Remoting;

public abstract class Spell : MonoBehaviour {

    public int recovery_time;//delai between spell
    public int mana_cost;
    public int recovery_max;
    public Cat cat;
    public KeyCode input;
    public Spell(Cat cat, KeyCode input)
    {
        this.cat = cat;
        this.input = input;
    }

    //delai de recovery
    //coup en mana
    //check
    public bool CanUse()
    {
		if(Input.GetKey (input) && recovery_time == recovery_max && mana_cost <=  cat.mana)
        {
            recovery_time = 0;
            cat.mana -= mana_cost;
            return true;
            
        }

        return false;
    }

	abstract public void Activate();
     
	
}
