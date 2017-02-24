using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Timers;

public class ChangeSceneTimer : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {

	}

    void StartMain()
    {
        Timer timer = new Timer();
        timer.Elapsed += LoadNextScene;
        timer.Interval = 800;
        timer.Enabled = true;
    }

    private void LoadNextScene(object source, ElapsedEventArgs e)
    {

    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
