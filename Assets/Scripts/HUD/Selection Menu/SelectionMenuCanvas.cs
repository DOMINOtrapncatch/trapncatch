using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class SelectionMenuCanvas : MonoBehaviour {

	[Header("Canvas fields")]
	public Text CatName;

	public Image TopLeftBar;
	public Image TopRightBar;
	public Image BottomLeftBar;
	public Image BottomRightBar;

	public List<Text> spellsNameTexts;
	public List<GameObject> spellsDescPanels;

	[Header("Characters Descriptions")]
	public List<CatDescription> catsDesc;
	public Chara_Selection chara_selection;

	////////
	/// HIDDEN FEATURES
	////////
	int lastCurrent = -1;

	void Update()
	{
		// Update if needed
		if (lastCurrent != chara_selection.cats_i)
		{
			// UPDATE - lastCurrent variable
			lastCurrent = chara_selection.cats_i;

			// UPDATE - Progress bars
			TopLeftBar.fillAmount 	  = catsDesc[lastCurrent].attack  / 100.0f;
			BottomLeftBar.fillAmount  = catsDesc[lastCurrent].defense / 100.0f;
			TopRightBar.fillAmount    = catsDesc[lastCurrent].speed   / 100.0f;
			BottomRightBar.fillAmount = catsDesc[lastCurrent].mana    / 100.0f;

			// UPDATE - Spells name
			for (int i = 0; i < catsDesc[lastCurrent].spellsName.Count; i++)
				spellsNameTexts[i].text = catsDesc[lastCurrent].spellsName[i];

			// UPDATE - Spells description
			for (int i = 0; i < catsDesc[lastCurrent].spellsDesc.Count; i++)
				spellsDescPanels[i].GetComponentInChildren<Text>().text = catsDesc[lastCurrent].spellsDesc[i];
		}
	}

	public void ShowSpellDesc(int pos)
	{
		spellsDescPanels[pos].SetActive(true);
	}

	public void HideSpellDesc(int pos)
	{
		spellsDescPanels[pos].SetActive(false);
	}

	[System.Serializable]
	public class CatDescription
	{
		public string name;

		[Range(0, 100)]
		public float attack;
		[Range(0, 100)]
		public float defense, speed, mana;

		public List<string> spellsName;
		public List<string> spellsDesc;
	}
}