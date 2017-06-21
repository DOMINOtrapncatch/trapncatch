using UnityEngine;

public class Splash : MonoBehaviour
{
    public string nextSceneName;
    public Color fadeColor = Color.white;

    void Awake()
    {
        ScenesManager.LoadSplash(nextSceneName, 1, 1, fadeColor);
    }

    void Update()
    {
        if(Input.GetButtonDown(SaveManager.GetInput("escape")))
            ScenesManager.ForceLoad(nextSceneName);
    }
}
