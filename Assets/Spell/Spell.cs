using UnityEngine;
using System.Collections;
using System.Runtime.Remoting;

public abstract class Spell : MonoBehaviour {

    public int mana_cost;
	public float recovery_time, recovery_max;
    public Cat cat;
    public KeyCode input;

    //delai de recovery
    //coup en mana
    //check
    public bool CanUse()
    {
		if(Input.GetKeyDown (input) && recovery_time == recovery_max && cat.mana - mana_cost >= 0)
		{
			print ("TRIGERRED");
            recovery_time = 0;
            cat.mana -= mana_cost;
            return true;
        }
        return false;
    }

	abstract public void Activate();
	
}
