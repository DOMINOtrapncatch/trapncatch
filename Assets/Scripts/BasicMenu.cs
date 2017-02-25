using UnityEngine;
using UnityEngine.UI;
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

    // When the button is being hovered
    public void Hover(string label)
    {
        Text text = GameObject.Find(label).GetComponent<Text>();
        text.color = new Color(1F, 1F, 1F, 1F);
        text.fontSize = 50;
    }

    // When the button is being unhovered
    public void UnHover(string label)
    {
        Text text = GameObject.Find(label).GetComponent<Text>();
        text.color = new Color(219F/255F, 211F/255F, 211F/255F, 1F);
        text.fontSize = 30;
    }
}
