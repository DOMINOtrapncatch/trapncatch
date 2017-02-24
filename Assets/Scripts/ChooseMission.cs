using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class ChooseMission : MonoBehaviour
{
	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
            SelectMission(true);
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
            SelectMission(false);
        else if (Input.GetKeyDown(KeyCode.Return))
            LaunchMission();
        else if (Input.GetKeyDown(KeyCode.Backspace))
            SceneManager.LoadScene(0);
    }

    // For the solo menu (true == right button, false == left button)
    public void SelectMission(bool direction)
    {
        Text Mission = GameObject.Find("MissionLabel").GetComponent<Text>();
        switch (Mission.text)
        {
            case "Chapitre 0: Une souris intriguante":
                if (direction)
                    Mission.text = "Chapitre 1 : Le chat de gouttière";
                else
                    Mission.text = "Chapitre 5 : To determine";
                break;
            case "Chapitre 1 : Le chat de gouttière":
                if (direction)
                    Mission.text = "Chapitre 2 : L'initiation";
                else
                    Mission.text = "Chapitre 0: Une souris intriguante";
                break;
            case "Chapitre 2 : L'initiation":
                if (direction)
                    Mission.text = "Chapitre 3 : De nouveaux arrivants";
                else
                    Mission.text = "Chapitre 1 : Le chat de gouttière";
                break;
            case "Chapitre 3 : De nouveaux arrivants":
                if (direction)
                    Mission.text = "Chapitre 4 : To determine";
                else
                    Mission.text = "Chapitre 2 : L'initiation";
                break;
            case "Chapitre 4 : To determine":
                if (direction)
                    Mission.text = "Chapitre 5 : To determine";
                else
                    Mission.text = "Chapitre 3 : De nouveaux arrivants";
                break;
            case "Chapitre 5 : To determine":
                if (direction)
                    Mission.text = "Chapitre 0: Une souris intriguante";
                else
                    Mission.text = "Chapitre 4 : To determine";
                break;
        }
    }

    // Load the selected mission
    public void LaunchMission()
    {
        Text Mission = GameObject.Find("MissionLabel").GetComponent<Text>();
        switch (Mission.text)
        {
            case "Chapitre 0: Une souris intriguante":
                SceneManager.LoadScene(0);
                break;
            case "Chapitre 1 : Le chat de gouttière":
                SceneManager.LoadScene(0);
                break;
            case "Chapitre 2 : L'initiation":
                SceneManager.LoadScene(0);
                break;
            case "Chapitre 3 : De nouveaux arrivants":
                SceneManager.LoadScene(0);
                break;
            case "Chapitre 4 : To determine":
                SceneManager.LoadScene(0);
                break;
            case "Chapitre 5 : To determine":
                SceneManager.LoadScene(0);
                break;
        }
    }
}
