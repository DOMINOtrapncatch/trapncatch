using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LogoLoader : MonoBehaviour {

	// Use this for initialization
	void Start () {
        StartCoroutine("Countdown");
	
	}

    private IEnumerator Countdown()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(0);
    }
}
