using UnityEngine;

public class PauseScript : BasicMenu
{
    GameObject PauseMenu;
    bool paused;
	int pauseTime = 0, pauseTime2 = 0;

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
		if(!paused)
		{
			pauseTime2++;

			if (Input.GetKey(KeyCode.Escape) && pauseTime2 > 10)
			{
				PauseMenu.SetActive(true);
				Time.timeScale = 0;
				paused = true;
				pauseTime2 = 0;
			}
		}
		else
		{
			pauseTime++;

			if(Input.GetKey(KeyCode.Escape) && pauseTime > 10)
			{
				PauseMenu.SetActive(false);
				Time.timeScale = 1;
				paused = false;
				pauseTime = 0;
			}
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
