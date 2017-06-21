using UnityEngine;

public static class InputManager
{
	public static bool GetKey(string key)
	{
		key = SaveManager.GetInput(key);

		try
        {
            return Input.GetKey(key);
        }
        catch
        {
			if (Input.inputString != "")
				return Input.inputString == key;
		}

		return false;
	}

    static string lastInput = "";

    public static bool GetKeyDown(string key)
    {
        key = SaveManager.GetInput(key);

		try
		{
            return Input.GetKeyDown(key);
		}
		catch
		{
			if (Input.inputString != "")
			{
				if (Input.inputString == lastInput)
				{
					lastInput = "";
				}
				else
				{
					lastInput = Input.inputString;
					return Input.inputString == key;
				}
			}
		}

		return false;
    }
}