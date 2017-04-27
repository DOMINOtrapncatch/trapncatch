using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using System;

public class BasicMenu : MonoBehaviour
{
	[Header("Main Menu")]
    public List<Text> labels = new List<Text>();
    public List<int> labelsSceneId = new List<int>();
	int currentSelectedLabel = 0;

	[Header("Mode Histoire")]
    public List <int> missionsSceneId = new List<int>();
    int currentMission = 0;

    void Update()
    {
		// Always show cursor into this scene
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

		// Handle inputs
		if (Input.GetKeyDown(KeyCode.DownArrow))
		{
			UnHover(labels[currentSelectedLabel]);
			currentSelectedLabel  = (currentSelectedLabel + 1) % labels.Count;
			Hover(labels[currentSelectedLabel]);
		}
		else if (Input.GetKeyDown(KeyCode.UpArrow))
		{
            UnHover(labels[currentSelectedLabel]);
			currentSelectedLabel  = (currentSelectedLabel - 1 + labels.Count) % labels.Count;
            Hover(labels[currentSelectedLabel]);
		}
		else if (Input.GetKeyDown(KeyCode.Return))
		{
            ChangeScene(labelsSceneId[currentSelectedLabel]);
		}
    }

    // Go from one scene to another scene specified by his id
    public void ChangeScene(int sceneId)
    {
		AutoFade.LoadLevel (sceneId, .3f, .3f, Palette.DARK_PURPLE);
    }

    //Go to the next mission
    public void NextMission()
    {
        currentMission = (currentMission + 1) % missionsSceneId.Count;
        ChangeScene(missionsSceneId[currentMission]);
    }

    // Close the game
    public void Quit()
    {
        Application.Quit();
    }

    // When the button is being hovered
    public void Hover(Text label)
    {
        label.color = new Color(1F, 1F, 1F, 1F);
        label.fontSize = 35;
    }

    // When the button is being hovered
    public void HoverSmall(Text label)
	{
		label.fontSize = 32;
    }

    // When the button is being unhovered
    public void UnHover(Text label)
    {
		label.color = Palette.LIGHT_GRAY;
        label.fontSize = 25;
    }

    // When the button is being unhovered
    public void UnHoverSmall(Text label)
	{
		label.fontSize = 30;
    }
}
