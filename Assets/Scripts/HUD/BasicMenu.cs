using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using System;

public class BasicMenu : MonoBehaviour
{
    public string[] names;
    string[] labelNames;
    string[] buttonNames;
    int buttonHovered = 0;
    public List <int> missionsSceneId = new List<int>();
    int currentMission = 0;

    

    void Start()
    {
        
        buttonNames = new string[names.Length];
        labelNames = new string[names.Length];

        for (int i = 0; i < buttonNames.Length; ++i)
            buttonNames[i] = names[i] + "Button";
        for (int i = 0; i < labelNames.Length; ++i)
            labelNames[i] = names[i] + "Label";
    }

    void Update()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        if (Input.GetKeyDown(KeyCode.DownArrow))
            HoverUpdate(1);
        else if (Input.GetKeyDown(KeyCode.UpArrow))
            HoverUpdate(-1);
        else if (Input.GetKeyDown(KeyCode.Return))
            Launch();
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
    public void Hover(string label)
    {
        Text text = GameObject.Find(label).GetComponent<Text>();
        text.color = new Color(1F, 1F, 1F, 1F);
        text.fontSize = 50;
    }

    // When the button is being unhovered
    public void UnHover(string label)
    {
        Text text = GameObject.Find(label).GetComponent<Text>();
		text.color = Palette.LIGHT_GRAY;
        text.fontSize = 30;
    }

    private void HoverUpdate(int direction)
    {
        UnHover(labelNames[buttonHovered]);
        buttonHovered = (buttonHovered + labelNames.Length + direction) % labelNames.Length;
        Hover(labelNames[buttonHovered]);
    }

    private void Launch()
    {
        Button button = GameObject.Find(buttonNames[buttonHovered]).GetComponent<Button>();
        if (button.interactable)
            button.onClick.Invoke();
    }
}
