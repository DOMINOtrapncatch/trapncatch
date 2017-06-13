using UnityEngine;
using System.Collections.Generic;

public class Mission3 : MissionBase
{ 
    [Header("Tooltips Configuration")]
    public string tooltip2 = "Tuez une dizaine de souris.";

    public List<Cat> allied;
    public List<Cat> ennemies;

    private bool alliedDead = false;
    private bool ennemiesDead = false;

    override public void CheckTooltips()
    {
        CheckDeath(allied);
        CheckDeath(ennemies);

        alliedDead = allied.Count == 0;
        ennemiesDead = ennemies.Count == 0;

        if (alliedDead)
            AutoFade.LoadLevel(10, .3f, .3f, Palette.GRAY);

        if (ennemiesDead)
        {
            AutoFade.LoadLevel(7, .3f, .3f, Palette.GRAY);
            SaveManager.Set("mission3", "1");
        }

        switch (currentTooltip)
        {
            case 1:
                if (CheckTooltip1())
                {
                    myHUD.SetObjective(tooltip2);
                    ++currentTooltip;
                }
                break;

            case 2:
                if (CheckTooltip2())
                {
                    ChooseMission.Success();
                    SaveManager.Set("mission3", "1");
                }
                break;
        }
    }

    // Check if swap has been used !
    bool CheckTooltip1()
    {
        return Swap.HasSwapped;
    }

    bool CheckTooltip2()
    {
        int killCount = 0;

        for (int i = 0; i < allied.Count; ++i)
            killCount += allied[i].enemyKillCount;

        return killCount >= 10;
    }

    void CheckDeath(List<Cat> cats)
    {
        for (int i = 0; i < cats.Count; ++i)
        {
            if (cats[i].Dead)
            {
                Cat toDestroy = cats[i];
                cats.Remove(cats[i]);
                toDestroy.Destroy();
            }
        }
    }
}
