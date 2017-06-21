using UnityEngine;

public abstract class Spell : Attack {

	[Header("HUD Handling")]
	public Sprite spellImage;

	[Header("Spell Capacities")]
	[Range(0, 100)]
    public float manaCost;

    // Caracteristics variables
    float maxRecoveryTime = 200;

	[SerializeField]
	[Range(0, 100)]
	int MaxRecoveryTimePercentage = 50;

	public float RecoveryTimeFillAmount { get { return RecoveryTime / MaxRecoveryTime; } }

	public float RecoveryTime { get; set; }
	public float RecoveryTimePercentage { get { return RecoveryTime / MaxRecoveryTime * 100.0f; } set { RecoveryTime = value * MaxRecoveryTime / 100.0f; } }
	public float MaxRecoveryTime { get { return MaxRecoveryTimePercentage * maxRecoveryTime / 100.0f; } }

	// Valeur permettant de savoir si le spell peut être activé
	public bool isActivable = false;

    protected override void MyUpdate()
    {
		if (RecoveryTime < MaxRecoveryTime)
			RecoveryTime++;

        base.MyUpdate();
	}

	public override bool CanUse()
    {
		if(RecoveryTime == MaxRecoveryTime && player.Mana - manaCost >= 0)
		{
            if(player.isActive && InputManager.GetKeyDown(inputString))
			{
				RecoveryTime = 0;

				if(manaCost > 0)
	            	player.Mana -= manaCost;

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

    public override void Activate()
    {
        player.OnSpellUsed(this);
    }
}
