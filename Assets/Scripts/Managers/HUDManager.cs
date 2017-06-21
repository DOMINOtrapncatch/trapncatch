using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class HUDManager : MonoBehaviour
{
	public List<Character> players = new List<Character>();
    int selectedPlayer;
    public Character player { get { return players.Count > 0 ? players[selectedPlayer] : null; } }

	protected List<Image> statusLoadingBars = new List<Image>();
	protected List<Image> spellsLoadingBars = new List<Image>();
	protected List<Image> spellsImage = new List<Image>();

	protected Image missionBackground;
    protected Text missionText;
    protected Image missionAdvancementBar;
    protected float defaultMissionTextPosY;

    public bool hasSwapped { get; private set; }

	void Awake()
	{
		missionBackground = transform.Find("StatusBox/MissionBackground").GetComponent<Image>();
		missionText = transform.Find("StatusBox/MissionBackground/MissionText").GetComponent<Text>();
		missionAdvancementBar = transform.Find("StatusBox/MissionBackground/MissionAdvancementBar").GetComponent<Image>();

		statusLoadingBars.Add(transform.Find("StatusBox/Status/Health/HealthBar").GetComponent<Image>());
		statusLoadingBars.Add(transform.Find("StatusBox/Status/Mana/ManaBar").GetComponent<Image>());

		for (int i = 1; i <= 4; i++)
			spellsLoadingBars.Add(transform.Find("RadialKey" + i + "/LoadingBar").GetComponent<Image>());

        Swap(0);

		UpdateHealth();
		UpdateSpell();
	}

	void Update()
	{
        CheckSwap();

		UpdateSpell();
		UpdateHealth();
		UpdateMana();
	}

    void CheckSwap()
    {
        if(InputManager.GetKeyDown("swap_up"))
        {
            hasSwapped = true;
            Swap(1);
        }
		else if (InputManager.GetKeyDown("swap_down"))
		{
			hasSwapped = true;
			Swap(-1);
		}
    }

    void Swap(int delta)
    {
        // Obtenir l'id du prochain chat
		selectedPlayer = (selectedPlayer + delta + players.Count) % players.Count;

        // Effectuer le swap
        for (int i = 0; i < players.Count; i++)
		{
            if(players[i] != null)
            {
				players[i].isActive = players[i] == player;
				players[i].gameObject.GetComponentInChildren<Camera>().enabled        = players[i] == player;
				players[i].gameObject.GetComponentInChildren<AudioListener>().enabled = players[i] == player;
				players[i].gameObject.GetComponent<PlayerController>().enabled        = players[i] == player;
				players[i].gameObject.GetComponentInChildren<Animator>().enabled      = players[i] == player;
            }
            else
            {
                players.RemoveAt(i);
                selectedPlayer = (selectedPlayer + delta + players.Count) % players.Count;
                i = 0;
            }
		}

        // Lancer le changement de gui
		LoadSelectedPlayer();
    }

    void LoadSelectedPlayer()
    {
		for (int i = 0; i < 4; i++)
		{
			// Disable/Enable all the unnecessary displayed spells
			if (i < player.spells.Count)
			{
				Image spellImage = transform.Find("RadialKey" + (i + 1) + "/Center/SpellImage").GetComponent<Image>();
				spellsImage.Add(spellImage);

				spellImage.sprite = player.spells[i].spellImage;
				spellsLoadingBars[i].transform.parent.gameObject.SetActive(true);
			}
			else
			{
				spellsLoadingBars[i].transform.parent.gameObject.SetActive(false);
			}
		}
    }

    public void OnPlayerDie(Character deadPlayer)
    {
		if (players.Contains(deadPlayer))
		{
			if (players.Count == 1)
            {
                HistoryMenu.MissionFail();
            }
            else
            {
				if (players.FindIndex(charact => charact == deadPlayer) == selectedPlayer)
					Swap(1);

				players.Remove(deadPlayer);
				selectedPlayer -= 1;
            }
		}
    }

	void UpdateSpell()
	{
		for (int i = 0; i < player.spells.Count; i++)
        {
            spellsLoadingBars[i].color = player.spells[i].isActivable ? Palette.SPELL_ACTIVE : Palette.SPELL_INACTIVE;
            spellsImage[i].color = player.spells[i].isActivable ? Palette.WHITE : Palette.GRAY;

            spellsLoadingBars[i].fillAmount = player.spells[i].RecoveryTimeFillAmount;
        }
	}

    Coroutine missionCoroutine;

	public void SetMission(int currentTooltip, bool isFirst = false)
	{
		int deltaMove = 50;
		if (isFirst)
			defaultMissionTextPosY = missionBackground.transform.localPosition.y - deltaMove;

        string missionTooltip = LanguageManager.GetMissionText(HistoryMenu.currentChapter, currentTooltip);

        if (missionText.text != missionTooltip)
		{
            if(missionCoroutine != null)
            {
                StopCoroutine(missionCoroutine);
                missionBackground.transform.localPosition = new Vector3(missionBackground.transform.localPosition.x, defaultMissionTextPosY, missionBackground.transform.localPosition.z);
            }

			missionCoroutine = StartCoroutine(ChangeMissionAnim(missionTooltip, isFirst, deltaMove));
		}
	}

	IEnumerator ChangeMissionAnim(string mission, bool isFirst, int deltaMove)
	{
        int animFrameCount = 20;

        float sourcePosY = missionBackground.transform.localPosition.y;

        if(!isFirst)
        {
			for (int animFrame = 1; animFrame <= animFrameCount; animFrame++)
			{
				missionBackground.transform.localPosition = new Vector3(missionBackground.transform.localPosition.x, sourcePosY + animFrame * deltaMove / animFrameCount, missionBackground.transform.localPosition.z);
				yield return new WaitForSeconds(0.001f);
			}
        }

        // Change mission
		missionAdvancementBar.fillAmount = 0;
		missionText.text = mission;
        yield return new WaitForSeconds(0.1f);

        sourcePosY = missionBackground.transform.localPosition.y;

		for (int animFrame = 1; animFrame <= animFrameCount; animFrame++)
		{
			missionBackground.transform.localPosition = new Vector3(missionBackground.transform.localPosition.x, sourcePosY - animFrame * deltaMove / animFrameCount, missionBackground.transform.localPosition.z);
			yield return new WaitForSeconds(0.001f);
		}
	}

    public void SetMissionAdvancementBar(float currentAdvancement, float maxAdvancement)
    {
        missionAdvancementBar.fillAmount = currentAdvancement / maxAdvancement;
    }

	void UpdateHealth()
	{
		statusLoadingBars[0].fillAmount = player.LifeFillAmount;
	}

	void UpdateMana()
	{
		statusLoadingBars[1].fillAmount = player.ManaFillAmount;
	}
}