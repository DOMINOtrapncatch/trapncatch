using UnityEngine;

public class Menu : MonoBehaviour
{
    // Loads a  scene
    public void LoadScene(string sceneName)
    {
        ScenesManager.LoadScene(sceneName, .3f, .3f, Palette.PURPLE);
	}

	// Exits the game
	public void Back()
	{
		ScenesManager.LoadLast(.3f, .3f, Palette.PURPLE);
	}

    // Exits the game
    public void Quit()
    {
        Application.Quit();
    }
}
