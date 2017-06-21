using UnityEngine;

public abstract class Mission : MonoBehaviour
{
	// HUD Handling
	protected HUDManager myHUD;

    // Tooltips variables
	protected int currentTooltip;

	// Use this for initialization
	void Start()
	{
		myHUD = GetComponent<HUDManager>();
        myHUD.SetMission(currentTooltip, true);
        OnLoad();
	}

	// Update is called once per frame
	void Update()
	{
        if(myHUD.player != null)
            CheckTooltips();
	}

    public virtual void OnLoad()
    {
        // NOTHING TO DO HERE BUT STILL OVERRIDEABLE
    }

	abstract public void CheckTooltips();
}
