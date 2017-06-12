using UnityEngine;
using System.Collections;
using System.Runtime.Remoting;
using UnityEngine.Networking;

public abstract class MSpell : NetworkBehaviour {

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
    public float recoveryTime { get { return RecoveryTime * maxRecoveryTime / 100; } }
    public float maxRecoveryTime { get { return MaxRecoveryTime * maxRecoveryTimeVal / 100; } }

    protected MCat cat;
    public string inputName;

    void Start()
    {
        cat = GetComponent<MCat>();

        InvokeRepeating("UpdateEverySecond", 0, 1.0f);
    }

    //delai de recovery
    //coup en mana
    //check
    public virtual bool CanUse()
    {
        if (Input.GetButtonDown(inputName) && recoveryTime == maxRecoveryTime && cat.Mana - manaCost >= 0)
        {
            RecoveryTime = 0;

            if (manaCost > 0)
                cat.Mana -= manaCost;

            return true;
        }

        return false;
    }

    abstract public void Activate();

    virtual public void UpdateEverySecond() { }
}
