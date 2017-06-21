using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Character : MonoBehaviour
{
    /*
     * Particle effects
     */
    [Header("Particle Effects")]
    public ParticleSystem deathParticle;

    /*
     * Max values are fixed values, common to every characters
     */
    float maxLife = 120, maxMana = 120, maxSpeed = 3, maxAttack = 40, maxDefense = 40;
    float maxManaRecoveryTime = 50;

    [Header("Character Caracteristics")]
	[SerializeField]
	[Range(0, 100)]
	int MaxLifePercentage = 100;
    [SerializeField]
    [Range(0, 100)]
	int MaxManaPercentage = 100, MaxSpeedPercentage = 100, MaxAttackPercentage = 100, MaxDefensePercentage = 100;
    [SerializeField]
    [Range(0, 100)]
    int MaxManaRecoveryTimePercentage = 100;

    /*
     * Events & Collision Detection lists
     */
    public List<Attack> attacks = new List<Attack>();
    public List<Spell> spells  = new List<Spell>();

	public List<GameObject> nearColliders { get; private set; }
	public List<Character>  nearEnemy     { get; private set; }
	public List<Character>  aroundEnemy   { get; private set; }

	HUDManager myHUD;
    public bool isActive { get; set; }

	public int lifeSpanSeconds;

	/*
     * HUD Fill Amount handling.
     */
    public float LifeFillAmount { get { return Life / MaxLife; } }
	public float ManaFillAmount { get { return Mana / MaxMana; } }

    /*
     * [PUBLIC] Real values: percentages applied to the max.
     */
	public float Life { get; set; }
	public float LifePercentage { get { return Life / MaxLife * 100.0f; } set { Life = value * MaxLife / 100.0f; } }
	public float MaxLife { get { return MaxLifePercentage * maxLife / 100.0f; } }

	public float Mana { get; set; }
	public float ManaPercentage { get { return Mana / MaxMana * 100.0f; } set { Mana = value * MaxMana / 100.0f; } }
	public float MaxMana { get { return MaxManaPercentage * maxMana / 100.0f; } }

	public float Speed { get; set; }
	public float SpeedPercentage { get { return Speed / MaxSpeed * 100.0f; } set { Speed = value * MaxSpeed / 100.0f; } }
	public float MaxSpeed { get { return MaxSpeedPercentage * maxSpeed / 100.0f; } }

    public float Attack { get; set; }
    public float AttackPercentage { get { return Attack / MaxAttack * 100.0f; } set { Attack = value * MaxAttack / 100.0f; } }
	public float MaxAttack  { get { return MaxAttackPercentage * maxAttack / 100.0f; } }

    public float Defense { get; set; }
	public float DefensePercentage { get { return Defense / MaxDefense * 100.0f; } set { Defense = value * MaxDefense / 100.0f; } }
	public float MaxDefense { get { return MaxDefensePercentage * maxDefense / 100.0f; } }

	public float ManaRecoveryTime { get; set; }
	public float ManaRecoveryTimePercentage { get { return ManaRecoveryTime / MaxManaRecoveryTime * 100.0f; } set { ManaRecoveryTime = value * MaxManaRecoveryTime / 100.0f; } }
	public float MaxManaRecoveryTime { get { return MaxManaRecoveryTimePercentage * maxManaRecoveryTime / 100.0f; } }

	/*
     * Statistics
     */
	public int enemyKillCount { get; private set; }
    public int spellsUseCount { get; private set; }

    void Awake()
    {
		aroundEnemy   = new List<Character>();
		nearColliders = new List<GameObject>();
		nearEnemy     = new List<Character>();

        myHUD = GameObject.Find("HUD").GetComponent<HUDManager>();
        lifeSpanSeconds = 0;

        // Init Real Values
		Life    = MaxLife;
		Mana    = MaxMana;
		Speed   = MaxSpeed;
		Attack  = MaxAttack;
		Defense = MaxDefense;
		ManaRecoveryTime = 0;

        InvokeRepeating("UpdateEverySecond", 0, 1.0f);
    }

    void Update()
    {
		CheckMana();
        Move();
	}

    void UpdateEverySecond()
    {
        lifeSpanSeconds += 1;
    }

    void CheckMana()
    {
		if (ManaRecoveryTime < MaxManaRecoveryTime)
        {
            ManaRecoveryTime++;
        }
        else
        {
            Mana++;
            ManaRecoveryTime = 0;
        }
    }

	/*
     * Events Triggers
     */
	public void OnSpellUsed(Spell spell)
	{
        if(spells.Contains(spell))
		    spellsUseCount++;
	}

	public virtual void OnEnemyKill(Character enemy)
	{
        enemyKillCount++;
	}

    /*
     * Attack functions
     */

	public virtual void Heal(float healAmount)
	{
        float newLife = Life + healAmount;

		if (newLife > MaxLife)
            newLife = MaxLife;

        Life = newLife;
	}

    public virtual void Damage(GameObject source, float damageAmount)
    {
		float newLife = Life - damageAmount;

        if (newLife <= 0)
			Death(source);

        Life = newLife;
    }

	public virtual void Death(GameObject source)
	{
        if(source != null)
        {
			Character character = (Character)source.GetComponent(typeof(Character));
			if (character != null)
			{
				character.OnEnemyKill(this);
				character.RemoveNearEnemy(this);
				character.RemoveAroundEnemy(this);
			}
			// INCOMING: else { if(POWER UP) }
		}

        myHUD.OnPlayerDie(this);
        
        ParticleSystem newDeathParticle = (ParticleSystem)Instantiate(deathParticle, transform.position, Quaternion.identity);
		Destroy(gameObject);
	}

	/*
     * [BOOSTS] && [MALUS]
     */
    public void ApplyBoost(BoostType boostType, float boostValue, float timeInSeconds)
    {
        StartCoroutine(Boost(boostType, boostValue, timeInSeconds, 1));
	}

	public void ApplyMalus(BoostType boostType, float malusValue, float timeInSeconds)
	{
        StartCoroutine(Boost(boostType, malusValue, timeInSeconds, -1));
	}

    IEnumerator Boost(BoostType boostType, float boostValue, float timeInSeconds, int bonusOrMalus)
    {
        switch(boostType)
        {
            case BoostType.LIFE:
                Life += boostValue * bonusOrMalus;
				break;
			case BoostType.MANA:
                Mana += boostValue * bonusOrMalus;
				break;
			case BoostType.SPEED:
                Speed += boostValue * bonusOrMalus;
				break;
            case BoostType.ATTACK:
                Attack += boostValue * bonusOrMalus;
				break;
            case BoostType.DEFENSE:
                Defense += boostValue * bonusOrMalus;
				break;
        }

        yield return new WaitForSeconds(timeInSeconds);

		switch (boostType)
		{
			case BoostType.LIFE:
				Life += boostValue * -bonusOrMalus;
				break;
			case BoostType.MANA:
				Mana += boostValue * -bonusOrMalus;
				break;
			case BoostType.SPEED:
				Speed += boostValue * -bonusOrMalus;
				break;
			case BoostType.ATTACK:
				Attack += boostValue * -bonusOrMalus;
				break;
			case BoostType.DEFENSE:
				Defense += boostValue * -bonusOrMalus;
				break;
		}
    }

    public enum BoostType { LIFE, MANA, SPEED, ATTACK, DEFENSE }

	/*
     * [MOVEMENT]
     */

	public bool isMovingAttack   { get; private set; }
    public bool isMovingForward  { get; private set; }
	public bool isMovingBackward { get; private set; }
	public bool isMovingRight    { get; private set; }
    public bool isMovingLeft     { get; private set; }
	public bool isMovingSprint   { get; private set; }
	public bool isMovingJump     { get; private set; }

    public virtual void Move()
	{
		isMovingAttack   = InputManager.GetKey("attack");
        isMovingForward  = InputManager.GetKey("up");
        isMovingBackward = InputManager.GetKey("down");
        isMovingRight    = InputManager.GetKey("right");
        isMovingLeft     = InputManager.GetKey("left");
		isMovingSprint   = InputManager.GetKey("sprint");
		isMovingJump     = InputManager.GetKey("jump");
    }

    /*
     * TRIGGERS PART
     */

	public virtual void AddAroundEnemy(Character enemy)
	{
        if (!aroundEnemy.Contains(enemy))
            aroundEnemy.Add(enemy);
	}

    public virtual void AddNearEnemy(Character enemy)
    {
        if (!nearEnemy.Contains(enemy))
        {
            if(aroundEnemy.Contains(enemy))
                aroundEnemy.Remove(enemy);

            nearEnemy.Add(enemy);
        }
	}

	public void AddCollider(GameObject collider)
	{
        if (!nearColliders.Contains(collider))
            nearColliders.Add(collider);
	}

	public virtual void RemoveAroundEnemy(Character enemy)
	{
        if (aroundEnemy.Contains(enemy))
            aroundEnemy.Remove(enemy);
	}

	public virtual void RemoveNearEnemy(Character enemy)
	{
        if (nearEnemy.Contains(enemy))
        {
            nearEnemy.Remove(enemy);
            aroundEnemy.Add(enemy);
        }
	}

	public void RemoveCollider(GameObject collider)
	{
        if (nearColliders.Contains(collider))
            nearColliders.Remove(collider);
	}
}
