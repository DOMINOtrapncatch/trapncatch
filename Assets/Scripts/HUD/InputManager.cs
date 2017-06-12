using System;
using System.Collections.Generic;

public static class InputManager {

	static List<string> inputValues = new List<string>{"spell1", "spell2", "spell3", "spell4", "attaque", "swap", "up", "right", "down", "left", "jump"};
	static List<RealKey> keys = new List<RealKey>();

	public static void Init(List<string> inputStrings)
	{
		foreach(string inputString in inputStrings)
			keys.Add(new RealKey(inputString, SaveManager.Get(inputString))); // TODO -> Load from save function (who gets form a save or default value)
	}

	public static RealKey Get(int inputID)
	{
		if(keys.Count == 0)
			Init(InitInputManager.inputValues);
		
		return keys[inputID];
	}

	public class RealKey
	{
		public string input;
		public string value;

		public RealKey(string input, string value)
		{
			this.input = input;
			this.value = value;
		}
	}
}