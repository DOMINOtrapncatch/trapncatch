using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ChooseMission : MonoBehaviour
{
	List<Mission> missions = new List<Mission>();
	int selectedMission = 0;

	// Use this for initialization
	void Start ()
    {
		missions.Add(new Mission(0, "Le chat de gouttière",  FindAudio("BGM0"), RGB(60 , 43 , 77)));
		missions.Add(new Mission(1, "L'initiation",          FindAudio("BGM1"), RGB(191, 112, 27)));
		missions.Add(new Mission(2, "Le chat de gouttière",  FindAudio("BGM2"), RGB(60 , 43 , 77)));
		missions.Add(new Mission(3, "De nouveaux arrivants", FindAudio("BGM3"), RGB(191, 112, 27)));
		missions.Add(new Mission(4, "To determine",          FindAudio("BGM4"), RGB(60 , 43 , 77)));
		missions.Add(new Mission(5, "To determine",          FindAudio("BGM5"), RGB(191, 112, 27)));
        missions[selectedMission].music.Play();
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

    // For the solo menu (1 == right button, -1 == left button)
    public void SelectMission(int direction)
    {
        Text missionLabel = GameObject.Find("MissionLabel").GetComponent<Text>();
        Image bgColor = GameObject.Find("BackgroundColor").GetComponent<Image>();

        missions[selectedMission].music.Stop();
        selectedMission = (selectedMission + direction + missions.Count) % missions.Count;
        missionLabel.text = "Chapitre " + missions[selectedMission].id + " : " + missions[selectedMission].title;
        missions[selectedMission].music.Play();
        bgColor.color = missions[selectedMission].color;
    }

    // Load the selected mission
    public void LaunchMission()
    {
		AutoFade.LoadLevel (missions[selectedMission].id + 5, .3f, .3f, Palette.DARK_PURPLE);
    }

    public void Back()
    {
		AutoFade.LoadLevel (1, .3f, .3f, Palette.DARK_PURPLE);
    }

    private AudioSource FindAudio(string name)
    {
        return GameObject.Find(name).GetComponent<AudioSource>();
    }

    private Color RGB(int r, int g, int b)
    {
        float red   = r / 255F;
        float green = g / 255F;
        float blue  = b / 255F;
        return new Color(red, green, blue, 1F);
    }
}


public class Mission
{
	public int id;
	public string title;
    public AudioSource music;
    public Color color;

    public Mission(int id, string title, AudioSource music, Color color)
	{
        this.music = new AudioSource();

		this.id = id;
		this.title = title;
        this.music = music;
        this.color = color;
	}
}