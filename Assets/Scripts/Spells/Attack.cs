using UnityEngine;

public abstract class Attack : MonoBehaviour
{
	protected Character player;

	[Header("Spell Input")]
	public string inputString;

	void Start()
	{
		player = (Character)GetComponent(typeof (Character));

		InvokeRepeating("UpdateEverySecond", 0, 1.0f);
	}

	void Update()
	{
        MyUpdate();
	}

    protected virtual void MyUpdate()
    {
		if (CanUse())
			Activate();
    }

    public virtual bool CanUse()
    {
        return player.isActive && InputManager.GetKeyDown(inputString);
    }

	abstract public void Activate();

	virtual public void UpdateEverySecond() { }
}