using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VisualNovelMenu : Menu
{
	public Text dialogText;
	public Image dialogBackground;

	[Header("Ressources")]
	public List<Sprite> dialogImages = new List<Sprite>();
	Dictionary<string, Sprite> dialogImagesDico = new Dictionary<string, Sprite>();

	int currentDialog = 0;

	void Awake()
	{
		foreach (Sprite dialogImage in dialogImages)
			dialogImagesDico.Add(dialogImage.name, dialogImage);

		ChangeDialog();
	}

	public void ChangeDialog()
	{
        if (SaveManager.ContainsVisualNovel(HistoryMenu.currentChapter, currentDialog, "image"))
        {
            dialogText.text = LanguageManager.GetVisualNovelText(HistoryMenu.currentChapter, currentDialog, "text");
            dialogBackground.sprite = dialogImagesDico[SaveManager.GetVisualNovel(HistoryMenu.currentChapter, currentDialog, "image")];

            // Go to the next dialog
            currentDialog += 1;
        }
        else
		{
			ScenesManager.LoadScene("Chapter_" + HistoryMenu.currentChapter, .3f, .3f, Color.black);
		}
	}
}
