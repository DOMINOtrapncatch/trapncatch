using UnityEngine;
using System.Collections.Generic;

public class InitInputManager : MonoBehaviour {

	public static List<string> inputValues = new List<string>{"spell1", "spell2", "spell3", "spell4", "attaque", "swap", "up", "right", "down", "left", "jump"};

	void Start()
	{
		InputManager.Init(inputValues);
	}
}