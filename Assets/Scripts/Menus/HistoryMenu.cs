using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class HistoryMenu : Menu
{
    [Header("Ressources")]
	public List<Mission> missions = new List<Mission>();

    [Header("GUI")]
	public Text chapterText;
	public Image chapterBackground;
	public AudioSource chapterAudioSource;

    public Text launchText;
    public Image launchTextForeground;

	public static int currentChapter;
	static int chaptersCount;

	void Awake()
    {
        chaptersCount = missions.Count;

        for (int chapterID = chaptersCount - 1; chapterID >= 0; chapterID--)
        {
            if (SaveManager.GetChapterState(chapterID) == 0)
            {
                currentChapter = chapterID;
                break;
            }
        }

        ChangeChapter(0);
    }

    public void ChangeChapter(int deltaChange)
    {
        currentChapter = (currentChapter + deltaChange + chaptersCount) % chaptersCount;

		chapterText.text = LanguageManager.GetMenuText("chapter") + " " + currentChapter + " :\n" + LanguageManager.GetChapterText(currentChapter, "title");

        chapterBackground.sprite = missions[currentChapter].image;

        chapterAudioSource.clip = missions[currentChapter].song;
        chapterAudioSource.Play();

        switch(SaveManager.GetChapterState(currentChapter))
        {
            case 1:
            case 0:
                launchText.text = LanguageManager.GetMenuText("launch");
                launchTextForeground.enabled = false;
                break;

            case -1:
				launchText.text = LanguageManager.GetMenuText("locked");
				launchTextForeground.enabled = true;
                break;
        }
	}

    public static void MissionSuccess()
	{
        if(!ScenesManager.isInTransition)
        {
			SaveManager.SetChapterState(currentChapter, 1);
			currentChapter++;
			ScenesManager.LoadScene((currentChapter + 1 >= chaptersCount) ? "Win" : "Success", .6f, .3f, Color.black);
        }
    }

    public static void MissionFail()
    {
        ScenesManager.LoadScene("Fail", .6f, .3f, Color.black);
    }

	public void LaunchMission()
	{
        ScenesManager.LoadScene("VisualNovel", .6f, .6f, Color.white);
	}

    public static void LaunchNextMission()
    {
        ScenesManager.LoadScene("VisualNovel", .6f, .6f, Color.white);
    }

    public static void LaunchCurrentChapter()
    {
        ScenesManager.LoadScene("VisualNovel", .6f, .6f, Color.white);
    }

    [System.Serializable]
    public class Mission
    {
        public Sprite image;
        public AudioClip song;
    }
}
