using UnityEngine;

public class PauseScript : BasicMenu
{
    GameObject PauseMenu;
    bool paused;

    // Use this for initialization
    void Start()
    {
        paused = false;
        PauseMenu = GameObject.Find("PauseMenu");

        PauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            if (!paused)
            {
                PauseMenu.SetActive(true);
                Time.timeScale = 0;
            }
            else
            {
                PauseMenu.SetActive(false);
                Time.timeScale = 1;
            }

            paused = !paused;
        }
    }

    public override void ChangeScene(int sceneId)
    {
        Time.timeScale = 1;
        base.ChangeScene(sceneId);
    }

    public void Resume()
    {
        PauseMenu.SetActive(false);
        Time.timeScale = 1;
        paused = false;
    }
}
