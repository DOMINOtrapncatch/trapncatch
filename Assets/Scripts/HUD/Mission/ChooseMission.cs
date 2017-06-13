using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ChooseMission : MonoBehaviour
{
	public List<Mission> missions = new List<Mission>();
	static List<Mission> missionsStatic = new List<Mission>();
	static int selectedMission = 0;

	// Use this for initialization
	void Start ()
    {
		if(missionsStatic.Count <= 0)
			foreach(Mission mission in missions)
				missionsStatic.Add(mission);

		selectedMission = 0;

        missions[selectedMission].music.Play();
        SelectMission(0);
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
            SelectMission(1);
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
            SelectMission(-1);
        else if (Input.GetKeyDown(KeyCode.Return))
            LaunchMission();
        else if (Input.GetKeyDown(KeyCode.Backspace) || Input.GetKeyDown(KeyCode.Escape))
            Back();
    }

    public static void Success()
    {
        AutoFade.LoadLevel(7, .3f, .3f, Color.black);
    }

    // For the solo menu (1 == right button, -1 == left button)
    public void SelectMission(int direction)
    {
        Text missionLabel = GameObject.Find("MissionLabel").GetComponent<Text>();
        Image bgColor = GameObject.Find("BackgroundColor").GetComponent<Image>();

		missions[selectedMission].music.Stop();

        selectedMission = (selectedMission + direction + missions.Count) % missions.Count;

		if(missions[selectedMission].image != null)
		{
			bgColor.sprite = missions[selectedMission].image;
			bgColor.color = Color.white;
		}
		else
		{
        	bgColor.color = missions[selectedMission].color;
		}

        missionLabel.text = "Chapitre " + selectedMission + " :\n" + missions[selectedMission].title;

        missions[selectedMission].music.Play();
    }

    // Load the selected mission
    public void LaunchMission()
    {
        string mission = "mission" + (selectedMission - 1);

        if (selectedMission == 0 || SaveManager.Get(mission) == "1")
	        AutoFade.LoadLevel (missions[selectedMission].id, .3f, .3f, Palette.DARK_PURPLE);
    }

    // Load the selected mission
    public static void NextMission()
	{
		selectedMission = (selectedMission + 1) % missionsStatic.Count;
		AutoFade.LoadLevel(missionsStatic[selectedMission].id, .3f, .3f, Palette.DARK_PURPLE);
	}

	// Restart the current mission
	public static void RestartMission()
	{
		AutoFade.LoadLevel(missionsStatic[selectedMission].id, .3f, .3f, Palette.DARK_PURPLE);
	}

    public void Back()
    {
		AutoFade.LoadLevel (1, .3f, .3f, Palette.DARK_PURPLE);
    }

	public void Hover(Text label)
	{
		label.fontSize += label.text.Length == 1 ? 15 : 5;
	}

	public void UnHover(Text label)
	{
		label.fontSize -= label.text.Length == 1 ? 15 : 5;
	}
}

[System.Serializable]
public class Mission
{
	public int id = 2;
	public string title;
    public AudioSource music;
	public Sprite image;
    public Color color;
}