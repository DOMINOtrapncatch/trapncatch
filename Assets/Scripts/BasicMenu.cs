using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class BasicMenu : MonoBehaviour
{
    // Go from one scene to another scene specified by his id
    public void ChangeScene(int sceneId)
    {
        SceneManager.LoadScene(sceneId);
    }

    // Close the game
    public void Quit()
    {
        Application.Quit();
    }
}
