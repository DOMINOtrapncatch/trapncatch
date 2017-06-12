using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Collections;

public class OptionsScript : BasicMenu {

	public List<Key> keys = new List<Key>();

	bool isEdittingValue = false;

	void Start ()
	{
		int i = 0;
		foreach(Key key in keys)
		{
			key.realKey = InputManager.Get(i);
			key.textBox.text = key.realKey.value;
			i++;
		}

		ColorDuplicates();
	}

	public void EditValue(Text key)
	{
		if(!isEdittingValue)
		{
			key.text = "> Press <";
			isEdittingValue = true;
			StartCoroutine(ChangeValue(key));
		}
	}

	IEnumerator ChangeValue(Text keyText)
	{
		Key keyToEdit = keys.Find(key => key.textBox == keyText);

		yield return new WaitForSeconds(0.1f); // Prevent left click from being detected

		while(true)
		{
			if (Input.anyKeyDown && Input.inputString != "")
			{
				keyToEdit.realKey.value = Input.inputString == " " ? "Space" : (Input.inputString == "\t" ? "Tab" : Input.inputString.ToUpper());
				break;
			}
			else if(Input.GetMouseButtonDown(0))
			{
				keyToEdit.realKey.value = "MOUSE 0";
				break;
			}
			else if(Input.GetMouseButtonDown(1))
			{
				keyToEdit.realKey.value = "MOUSE 1";
				break;
			}
			yield return new WaitForSeconds(0.01f);
		}

		keyText.text = keyToEdit.realKey.value;
        SaveManager.Set(keyToEdit.realKey.input, keyToEdit.realKey.value);

		ColorDuplicates();

		isEdittingValue = false;
	}

	void ColorDuplicates()
	{
		Dictionary<string, List<Key>> duplicates = new Dictionary<string, List<Key>>();

		foreach(Key key in keys)
		{
			if(!duplicates.ContainsKey(key.realKey.value))
			{
				duplicates.Add(key.realKey.value, new List<Key>{key});
			}
			else
			{
				duplicates[key.realKey.value].Add(key);
			}

			key.textBox.color = Palette.DARK_GRAY;
		}

		foreach(KeyValuePair<string, List<Key>> duplicate in duplicates)
		{
			if(duplicate.Value.Count > 1)
			{
				foreach(Key key in duplicate.Value)
				{
					key.textBox.color = Palette.RED;
				}
			}
		}
	}

	[System.Serializable]
	public class Key
	{
		[HideInInspector]
		public InputManager.RealKey realKey;
		public Text textBox;
	}
}
