using System;
using System.Collections.Generic;

public static class InputManager {

	static List<RealKey> keys = new List<RealKey>();

	public static void Init(List<string> inputStrings)
	{
		foreach(string inputString in inputStrings)
			keys.Add(new RealKey(inputString, "a")); // TODO -> Load from save function (who gets form a save or default value)
	}

	public static RealKey Get(int inputID)
	{
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