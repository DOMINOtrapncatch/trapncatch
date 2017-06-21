using System.Collections.Generic;
using UnityEngine;

public class Start : MonoBehaviour
{
    public List<string> scenesToLaunch = new List<string>();

    void Awake()
    {
        foreach(string sceneToLaunch in scenesToLaunch)
		    ScenesManager.LoadFirst(sceneToLaunch, 1, Color.white);
    }
}
