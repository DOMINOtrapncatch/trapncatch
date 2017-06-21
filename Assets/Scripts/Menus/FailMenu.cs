using UnityEngine;
using System.Collections;

public class FailMenu : Menu
{
    public void RestartMission()
    {
        HistoryMenu.LaunchCurrentChapter();
    }
}
