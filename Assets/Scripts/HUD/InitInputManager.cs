using UnityEngine;
using System.Collections.Generic;

public class InitInputManager : MonoBehaviour {

	public static List<string> inputValues = new List<string>{"spell 1", "spell 2", "spell 3", "spell 4", "attaque", "swap", "up", "right", "down", "left", "jump"};

	void Start()
	{
		InputManager.Init(inputValues);
	}
}