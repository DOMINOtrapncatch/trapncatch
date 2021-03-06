﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class DialogueScript : MonoBehaviour
{
    public string[] dialogueTexts;
    public Sprite[] dialogueImages;
    public int sceneId;
    Image dialogueImage;
    Text dialogueText;
    int click = 0;

	// Use this for initialization
	void Start ()
    {
        dialogueText = transform.Find("Dialogue/DialogueText").GetComponent<Text>();
        dialogueImage = transform.Find("Background").GetComponent<Image>();
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
            AutoFade.LoadLevel(sceneId, .3f, .3f, Color.black);
        ++click;
    }

	public void Hover(Text label)
	{
		label.fontSize += 10;
	}

	public void UnHover(Text label)
	{
		label.fontSize -= 10;
	}
}
