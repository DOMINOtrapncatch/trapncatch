using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DialogueMission1 : MonoBehaviour
{
    public Text dialogueStr;
    string dialogueTxt;
    int click;

	// Use this for initialization
	void Start ()
    {
        dialogueStr = GameObject.Find("DialogueText").GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        dialogueStr.text = dialogueTxt;

        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
            Proceed();
        if (click == 0)
            dialogueTxt = "*MIAOUU*";
        if (click == 1)
            dialogueTxt = "Oh une souris!";
        if (click == 2)
            dialogueTxt = "Je vais un peu m'amuser avec elle...";
    }

    public void Proceed()
    {
        click++;
    }
}
