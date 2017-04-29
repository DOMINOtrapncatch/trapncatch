using UnityEngine;

abstract public class MissionBase : MonoBehaviour
{
	// HUD Handling
	protected HUDManager myHUD;

	// Tooltips variables
	public string messageTooltip1;

	// Use this for initialization
	void Start()
	{
		myHUD = GetComponent<HUDManager>();
		myHUD.SetObjective(messageTooltip1);
	}

	// Update is called once per frame
	void Update()
	{
		CheckTooltips();
	}

	abstract public void CheckTooltips();
}