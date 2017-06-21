using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class SaveManager : MonoBehaviour
{
	[SerializeField]
	List<ChapterData> chapters = new List<ChapterData>();
	[SerializeField]
	List<MenuData> menus = new List<MenuData>();
	[SerializeField]
	List<InputData> inputs = new List<InputData>();

	static Dictionary<string, string> defaultDatasStatic = new Dictionary<string, string>();
	static Dictionary<string, string> datas = new Dictionary<string, string>();
	static string fileName = "data.sav";

	void Awake()
	{
		int chapterID = 0;
		foreach (ChapterData chapter in chapters)
		{
			string chapterString = "chapter_" + chapterID;

            CreateData(chapterString + ".state", chapter.state + "");

			if (chapter.title.Count >= LanguageManager.possibleLanguages.Count)
			{
				int languageID = 0;
				foreach (string title in chapter.title)
				{
					CreateData(chapterString + ".title." + LanguageManager.possibleLanguages[languageID], title);
					languageID++;
				}
			}
			else if (chapter.title.Count > 0)
			{
				int languageID = 0;
				foreach (string language in LanguageManager.possibleLanguages)
				{
					CreateData(chapterString + ".title." + language, (chapter.title.Count > languageID ? chapter.title[languageID] : chapter.title[chapter.title.Count - 1]));
					languageID++;
				}
			}

			int visualNovelID = 0;
			foreach (VisualNovelData visualNovel in chapter.visualNovels)
			{
				string visualNovelString = "visualnovel_" + visualNovelID;

				if (visualNovel.text.Count >= LanguageManager.possibleLanguages.Count)
				{
					int languageID = 0;
					foreach (string title in visualNovel.text)
					{
						CreateData(chapterString + "." + visualNovelString + ".text." + LanguageManager.possibleLanguages[languageID], title);
						languageID++;
					}
				}
				else if (visualNovel.text.Count > 0)
				{
					int languageID = 0;
					foreach (string language in LanguageManager.possibleLanguages)
					{
						CreateData(chapterString + "." + visualNovelString + ".text." + language, (visualNovel.text.Count > languageID ? visualNovel.text[languageID] : visualNovel.text[visualNovel.text.Count - 1]));
						languageID++;
					}
				}

				CreateData(chapterString + "." + visualNovelString + ".image", visualNovel.image);

				visualNovelID++;
			}

			int missionID = 0;
			foreach (MissionData mission in chapter.missions)
			{
				string missionString = "mission_" + missionID;

				if (mission.text.Count >= LanguageManager.possibleLanguages.Count)
				{
					int languageID = 0;
					foreach (string title in mission.text)
					{
						CreateData(chapterString + "." + missionString + ".text." + LanguageManager.possibleLanguages[languageID], title);
						languageID++;
					}
				}
				else if (mission.text.Count > 0)
				{
					int languageID = 0;
					foreach (string language in LanguageManager.possibleLanguages)
					{
						CreateData(chapterString + "." + missionString + ".text." + language, (mission.text.Count > languageID ? mission.text[languageID] : mission.text[mission.text.Count - 1]));
						languageID++;
					}
				}

				missionID++;
			}

			chapterID++;
		}

		foreach (MenuData menu in menus)
		{
			string menuString = "menu." + menu.key;

			if (menu.text.Count >= LanguageManager.possibleLanguages.Count)
			{
				int languageID = 0;
				foreach (string text in menu.text)
				{
					CreateData(menuString + "." + LanguageManager.possibleLanguages[languageID], text);
					languageID++;
				}
			}
			else if (menu.text.Count > 0)
			{
				int languageID = 0;
				foreach (string language in LanguageManager.possibleLanguages)
				{
					CreateData(menuString + "." + language, (menu.text.Count > languageID ? menu.text[languageID] : menu.text[menu.text.Count - 1]));
					languageID++;
				}
			}

		}

		foreach (InputData input in inputs)
		{
			string inputString = "input." + input.key;

			CreateData(inputString, input.input);
		}

		BackupData("backup_3");
		CreateSave();
		LoadSave();
	}

	// Save the data into default and current
	void CreateData(string key, string val)
	{
		defaultDatasStatic.Add(key, val);
		datas.Add(key, val);
	}

	// Create the file if needed
	void CreateSave()
	{
		if (!File.Exists(fileName))
            File.Create(fileName);
	}

	// Update data
	void LoadSave()
	{
		string[] lines = File.ReadAllText(fileName).Split('\n');

		for (int i = 0; i < lines.Length; ++i)
		{
			string[] lineValues = lines[i].Split('=');

			if (lineValues.Length == 2 && datas.ContainsKey(lineValues[0]))
				datas[lineValues[0]] = lineValues[1];
		}
	}

	static void BackupData(string backupName)
	{
		if (File.Exists(backupName) && File.ReadAllText(backupName).Length == 0)
		{
			string newData = "";

			foreach (KeyValuePair<string, string> data in datas)
				newData += data.Key + "=" + data.Value + "\n";

			File.WriteAllText(backupName, newData);
		}
		else
		{
			File.Create(backupName);
		}
	}

	// Writes specific data into the file
	static void WriteData()
	{
		string newData = "";

        for (int i = 0; i < datas.Count; i++)
        {
			string key = datas.ElementAt(i).Key;
			string val = datas.ElementAt(i).Value;

            if(defaultDatasStatic.ContainsKey(key) && defaultDatasStatic[key] != val)
                newData += key + "=" + val + "\n";
        }

		File.WriteAllText(fileName, newData);
	}

	public static void Set(string key1, string val1, string key2, string val2)
	{
		// Set local variable
		datas[key1] = val1;
		datas[key2] = val2;

		// Set file variable
		WriteData();
	}

	public static void Set(string key, string val)
	{
		// Set local variable
		datas[key] = val;

		// Set file variable
		WriteData();
	}

	public static void SetVisualNovel(int chapter, int visualNovel, string item, string val)
	{
		Set("chapter_" + chapter + ".visualnovel_" + visualNovel + "." + item, val);
	}

	public static void SetChapterState(int chapter, int state)
	{
        // Unlock next chapter if necessary and if exists
        if(state == 1 && ContainsChapter(chapter + 1, "state") && GetChapter(chapter + 1, "state") == "-1")
            Set("chapter_" + chapter + ".state", state + "", "chapter_" + (chapter + 1) + ".state", "0");
        else
            Set("chapter_" + chapter + ".state", state + "");
	}

	public static void SetChapter(int chapter, string item, string val)
	{
		Set("chapter_" + chapter + "." + item, val);
	}

	public static void SetMenu(string item, string val)
	{
		Set("menu." + item, val);
	}

	public static void SetInput(string item, string val)
	{
		Set("input." + item, val);
	}

	public static string Get(string key)
	{
        if (!ContainsData(key))
			return key;

		// Get local variable
		return datas[key];
	}

	public static string GetVisualNovel(int chapter, int visualNovel, string item)
	{
		return Get("chapter_" + chapter + ".visualnovel_" + visualNovel + "." + item);
	}

	public static int GetChapterState(int chapter)
	{
        int state = -1;

        try
        {
            state = int.Parse(Get("chapter_" + chapter + ".state"));
        }
        catch {}

        return state;
	}

	public static string GetChapter(int chapter, string item)
	{
		return Get("chapter_" + chapter + "." + item);
	}

	public static string GetMenu(string item)
	{
		return Get("menu." + item);
	}

	public static string GetInput(string item)
	{
		return Get("input." + item);
	}

    static bool ContainsData(string key)
    {
        return datas.ContainsKey(key);
    }

    public static bool ContainsVisualNovel(int chapter, int visualNovel, string item)
    {
        return ContainsData("chapter_" + chapter + ".visualnovel_" + visualNovel + "." + item);
    }

    public static bool ContainsChapter(int chapter, string item)
    {
        return ContainsData("chapter_" + chapter + "." + item);
    }
}

[System.Serializable]
public class VisualNovelData
{
	public List<string> text = new List<string>();
	public string image = "";
}

[System.Serializable]
public class MissionData
{
	public List<string> text = new List<string>();
}

[System.Serializable]
public class ChapterData
{
    public int state = -1; // -1 = Not Accessible | 0 = Accessible | 1 = Done
	public List<string> title = new List<string>();
	public List<VisualNovelData> visualNovels = new List<VisualNovelData>();
	public List<MissionData> missions = new List<MissionData>();
}

[System.Serializable]
public class MenuData
{
	public string key = "";
	public List<string> text = new List<string>();
}

[System.Serializable]
public class InputData
{
	public string key = "", input = "";
}