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
        yield return new WaitForSeconds(4);
		switch(loadScene)
		{
			case 4:
				AutoFade.LoadLevel (loadScene, 2, 2, Color.white);
				break;
			case 1:
				AutoFade.LoadLevel (loadScene, 2, 2, Palette.DARK_PURPLE);
				break;
			default:
				AutoFade.LoadLevel (loadScene, 2, 2, Color.black);
				break;
		}
    }
}
