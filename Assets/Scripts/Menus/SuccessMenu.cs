using UnityEngine;
using System.Collections;

public class SuccessMenu : Menu
{
    void Awake()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
	}

    public void NextMission()
    {
        HistoryMenu.LaunchNextMission();
    }
}
