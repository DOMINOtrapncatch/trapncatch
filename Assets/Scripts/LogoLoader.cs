using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LogoLoader : MonoBehaviour {

	public int loadScene;

	// Use this for initialization
	void Start ()
    {
        StartCoroutine("Countdown");
	}

    private IEnumerator Countdown()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene(loadScene);
    }
}
