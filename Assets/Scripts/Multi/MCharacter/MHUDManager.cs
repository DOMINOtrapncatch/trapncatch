using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

public class MHUDManager : NetworkBehaviour
{
    public MCat player;
    List<Image> statusUI = new List<Image>();
    List<Image> spellsUI = new List<Image>();
    Text objectiveUI;
    public Text scoreText;
    public Text countdown;

    public Timer count;
    [SyncVar]
    public int minPlayers;
    [SyncVar]
    public float timer;

    void Awake()
    {
        this.objectiveUI = transform.Find("StatusBox/Panel/Text").GetComponent<Text>();
        this.scoreText = transform.Find("score").GetComponent<Text>();
        this.countdown = transform.Find("countdown").GetComponent<Text>();
        this.count = transform.Find("countdown").GetComponent<Timer>();

        this.statusUI.Add(transform.Find("StatusBox/Status/Health/HealthBar").GetComponent<Image>());
        this.statusUI.Add(transform.Find("StatusBox/Status/Mana/ManaBar").GetComponent<Image>());

        for (int i = 1; i <= 4; i++)
            this.spellsUI.Add(transform.Find("RadialKey" + i + "/LoadingBar").GetComponent<Image>());

        if(player != null)
            LoadPlayer(player);

        UpdateHealth();
        UpdateSpell();
    }

    void Start()
    {
        if (isServer)
        {
            
            if (isLocalPlayer)
            {
                timer = count.timelimit;
            }
        }
        else if(isLocalPlayer)
        {
            RpcUI();
        }
    }
    void Update()
    {
        if (!isLocalPlayer)
            return;

        if(isServer)
        {
            MasterControl();
        }
        UpdateSpell();
        UpdateHealth();
        UpdateMana();

        
        ClientTimeUpdate();
        RpcUI();
    }

    public void LoadPlayer(MCat cat)
    {
        this.player = cat;

        for (int i = 1; i <= (cat.spells.Count > 4 ? 4 : cat.spells.Count); i++)
        {
            Image spellImage = transform.Find("RadialKey" + i + "/Center/Image").GetComponent<Image>();

            if (cat.spells.Count >= i)
            {
                spellImage.sprite = player.spells[i - 1].image;
                spellsUI[i - 1].transform.parent.gameObject.SetActive(true);
            }
            else
                spellsUI[i - 1].transform.parent.gameObject.SetActive(false);
        }
    }

    private void UpdateSpell()
    {
        for (int i = 0; i < player.spells.Count; i++)
        {
            if (player.spells[i].recoveryTime < player.spells[i].maxRecoveryTime)
            {
                player.spells[i].RecoveryTime += 1;
                spellsUI[i].color = new Color(spellsUI[i].color.r, spellsUI[i].color.g, spellsUI[i].color.b, 20);
            }
            else
            {
                spellsUI[i].color = new Color(spellsUI[i].color.r, spellsUI[i].color.g, spellsUI[i].color.b, 100);
            }

            spellsUI[i].fillAmount = player.spells[i].recoveryTime / player.spells[i].maxRecoveryTime;
        }
    }

    public void SetObjective(string objective)
    {
        objectiveUI.text = objective;
    }

    private void UpdateHealth()
    {
        statusUI[0].fillAmount = player.life / player.maxLife;
    }

    private void UpdateMana()
    {
        statusUI[1].fillAmount = player.mana / player.maxMana;

        if (player.mana < player.maxMana)
        {
            if (player.ManaRecoveryTime == player.manaMaxRecoveryTime)
            {
                player.ManaRecoveryTime = 0;
                player.Mana += 1;
            }
            else
            {
                player.ManaRecoveryTime += 1;
            }
        }
    }

    public void DisplayScore(int score)
    {
        scoreText.text = string.Format("Score : {0}", score);
    }

    #region timer

    public void MasterControl()
    {
       
            if(timer <= 0)
            {
                timer = -2;
            }
            else if(timer == -1)
            {
                if (NetworkServer.connections.Count >= minPlayers)
                    timer = 0;
            }
            else if(timer == -2)
            {
                //call customnunet for load end scene
            }
            else
            {
                count.TimeControl();
            }
        
    }

    public void ClientTimeUpdate()
    {
        if (isServer)
            return;

        RpcUI();
    }

    
    public void ServerControl()
    {
        count.TimeUI();
        count.LastMinuteRed();
    }

    [ClientRpc]
    public void RpcUI()
    {
        ServerControl();
    }
    #endregion
}