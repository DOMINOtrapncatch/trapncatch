﻿using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ChooseMission : MonoBehaviour
{
	public List<Mission> missions = new List<Mission>();
	int selectedMission = 0;

	// Use this for initialization
	void Start ()
    {
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
		AutoFade.LoadLevel (missions[selectedMission].id + 5, .3f, .3f, Palette.DARK_PURPLE);
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
	public int id;
	public string title;
    public AudioSource music;
	public Sprite image;
    public Color color;
}