using UnityEngine;
using System.Collections;

public abstract class Spell {

    public int recovery_time;//delai between spell
    public int mana_cost;
    public int recovery_max;
    public Cat cat;
    public Input input;
    public Spell(Cat cat, Input input)
    {
        this.cat = cat;
        this.input = input;
    }

    //delai de recovery
    //coup en mana
    //check
    public bool CanUse()
    {
        if(recovery_time == recovery_max && mana_cost <=  cat.mana)
        {
            recovery_time = 0;
            cat.mana -= mana_cost;
            return true;
            
        }

        return false;
        
    }

    
     
	
}
