using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class DialogueScript : MonoBehaviour
{
    public string dialogueLabel;
    public string[] dialogueTexts;
    public int sceneId;
    Text dialogueText;
    int click = 0;

	// Use this for initialization
	void Start ()
    {
        dialogueText = GameObject.Find(dialogueLabel).GetComponent<Text>();
        Proceed();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
            Proceed();
    }

    public void Proceed()
    {
        if (click < dialogueTexts.Length)
            dialogueText.text = dialogueTexts[click];
        else
            SceneManager.LoadScene(sceneId);
        ++click;
    }
}
