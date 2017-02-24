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
		missions.Add(new Mission(0, "Le chat de gouttière"));
		missions.Add(new Mission(1, "L'initiation"));
		missions.Add(new Mission(2, "Le chat de gouttière"));
		missions.Add(new Mission(3, "De nouveaux arrivants"));
		missions.Add(new Mission(4, "To determine"));
		missions.Add(new Mission(5, "To determine"));
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
        else if (Input.GetKeyDown(KeyCode.Backspace))
            SceneManager.LoadScene(0);
    }

    // For the solo menu (1 == right button, -1 == left button)
    public void SelectMission(int direction)
    {
        Text missionLabel = GameObject.Find("MissionLabel").GetComponent<Text>();
		selectedMission = (selectedMission + direction + missions.Count) % missions.Count;
		missionLabel.text = "Chapitre " + missions[selectedMission].id + " : " + missions[selectedMission].title;
    }

    // Load the selected mission
    public void LaunchMission()
    {
		SceneManager.LoadScene(selectedMission);
    }
}


public class Mission
{
	public int id;
	public string title;

	public Mission(int id, string title)
	{
		this.id = id;
		this.title = title;
	}
}