using UnityEngine;
﻿using System;
using System.Collections.Generic;

public static class InputManager {

	static List<RealKey> keys = new List<RealKey>();

	public static void Init(List<string> inputStrings)
	{
		foreach(string inputString in inputStrings)
            keys.Add(new RealKey(inputString, SaveManager.Get(inputString)));
	}

    public static string Get(string inputString)
	{
		if (keys.Count == 0)
			Init(InitInputManager.inputValues);

		return keys.Find(obj => obj.input == inputString).value;
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