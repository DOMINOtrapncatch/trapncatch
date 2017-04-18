using UnityEngine;
using System.Collections;
using System.Runtime.Remoting;

public abstract class Spell : MonoBehaviour {

    public float manaCost;

	// Variables qui pourront etres modifiees par l'utilisateur
	[Range(0, 100)]
	public float RecoveryTime;
	[Range(0, 100)]
	public float MaxRecoveryTime;

	// Valeur maximales brutes
	private float maxRecoveryTimeVal = 100;

	// Variables utilisees dans les scripts
	public float recoveryTime    { get { return RecoveryTime    * maxRecoveryTime    / 100; } }
	public float maxRecoveryTime { get { return MaxRecoveryTime * maxRecoveryTimeVal / 100; } }

    public Cat cat;
    public string inputName;

    //delai de recovery
    //coup en mana
    //check
    public bool CanUse()
    {
		if(Input.GetButtonDown (inputName) && recoveryTime == maxRecoveryTime && cat.mana - manaCost >= 0)
		{
            RecoveryTime = 0;

			if(manaCost > 0)
            	cat.Mana -= manaCost;
			
            return true;
        }

        return false;
    }

	abstract public void Activate();
	
}
