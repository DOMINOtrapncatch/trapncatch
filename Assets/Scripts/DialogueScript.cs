using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class DialogueScript : MonoBehaviour
{
    public string dialogueLabel;
    public string bgImage;
    public string[] dialogueTexts;
    public Sprite[] dialogueImages;
    public int sceneId;
    Image dialogueImage;
    Text dialogueText;
    int click = 0;

	// Use this for initialization
	void Start ()
    {
        dialogueText = GameObject.Find(dialogueLabel).GetComponent<Text>();
        dialogueImage = GameObject.Find(bgImage).GetComponent<Image>();
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
        {
            dialogueText.text = dialogueTexts[click];
            if (click < dialogueImages.Length)
                dialogueImage.sprite = dialogueImages[click];
        }
        else
            SceneManager.LoadScene(sceneId);
        ++click;
    }
}
