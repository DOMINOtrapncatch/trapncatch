using System.Collections.Generic;
using UnityEngine;

public class LanguageManager : MonoBehaviour
{
	public static List<string> possibleLanguages = new List<string> { "french", "english" };
	public static string selectedLanguage;

	void Awake()
	{
		LoadLanguage();
	}

	// Load the saved language from save file
	void LoadLanguage()
	{
		string language = SaveManager.Get("language");

		if (language != null && possibleLanguages.Contains(language))
			selectedLanguage = language;
		else
			selectedLanguage = possibleLanguages[0];
	}

	static string GetText(string stringKey)
	{
		return SaveManager.Get(stringKey + "." + selectedLanguage);
	}

	public static string GetVisualNovelText(int chapter, int visualNovel, string stringKey)
	{
		return GetText("chapter_" + chapter + ".visualnovel_" + visualNovel + "." + stringKey);
	}

	public static string GetMissionText(int chapter, int mission)
	{
		return GetText("chapter_" + chapter + ".mission_" + mission + ".text");
	}

	public static string GetChapterText(int chapter, string stringKey)
	{
		return GetText("chapter_" + chapter + "." + stringKey);
	}

	public static string GetMenuText(string stringKey)
	{
		return GetText("menu." + stringKey);
	}

	public static string GetInputText(string stringKey)
	{
		return GetText("input." + stringKey);
	}
}
