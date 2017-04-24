using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class AdminBoxScript : MonoBehaviour
{
    public Button.ButtonClickedEvent[] btEvent;
    public string[] btName;
    public GameObject panel;
    private RectTransform panelRect;

    void Start()
    {
        if (btEvent.Length != btName.Length)
            throw new Exception("SAME LENGTH !!!");

        int height = 0;
        panelRect = panel.GetComponent<RectTransform>();
        panel.SetActive(false);
        
        for (int i = 0; i < btName.Length; ++i)
        {
            var button = Instantiate(Resources.Load("AdminDefaultButton"), panelRect) as GameObject;
            button.GetComponent<RectTransform>().localPosition = new Vector3(0, -height, 0);
            button.GetComponentInChildren<Text>().text = btName[i];
            button.GetComponentInChildren<Button>().onClick = btEvent[i];
            height += 25;
            panelRect.sizeDelta = new Vector2(150, height);
        }
    }

    public void ShowPanel()
    {
        panel.SetActive(!panel.activeSelf);
    }

    public void RestartMission()
    {
        AutoFade.LoadLevel(SceneManager.GetActiveScene().buildIndex, .3f, .3f, Palette.WHITE);
    }
}