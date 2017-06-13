using UnityEngine;
using System.Collections;
using System.Runtime.Remoting;

public abstract class Spell : MonoBehaviour {

	[Header("HUD Handling")]
	public Sprite image;

	[Header("Spell capacities")]
	[Range(0, 100)]
    public float manaCost;

	// Variables qui pourront etres modifiees par l'utilisateur
	[Range(0, 100)]
	public float MaxRecoveryTime;

	// Variables qui ne pourront pas etres modifiees par l'utilisateur
	[HideInInspector]
	public float RecoveryTime;

	// Valeur maximales brutes
	private float maxRecoveryTimeVal = 100;

	// Variables utilisees dans les scripts
	public float recoveryTime    { get { return RecoveryTime    * maxRecoveryTime    / 100; } }
	public float maxRecoveryTime { get { return MaxRecoveryTime * maxRecoveryTimeVal / 100; } }

	protected Cat cat;
    public string inputName;

	// Valeur permettant de savoir si le spell peut être activé
	public bool isActivable = false;

	void Start()
	{
		cat = GetComponent<Cat>();

        InvokeRepeating("UpdateEverySecond", 0, 1.0f);
	}

    //delai de recovery
    //coup en mana
    //check
	public virtual bool CanUse()
    {
		if(recoveryTime == maxRecoveryTime && cat.Mana - manaCost >= 0)
		{
            if(Input.GetButtonDown(InputManager.Get(inputName)))
			{
				RecoveryTime = 0;

				if(manaCost > 0)
	            	cat.Mana -= manaCost;

				isActivable = false;
				
	            return true;
			}
			else
			{
				isActivable = true;
			}
        }

        return false;
    }

	abstract public void Activate();

	virtual public void UpdateEverySecond() {}
}
