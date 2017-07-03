using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

    public Text countdown;
    public float timelimit;//minute
    public float second;
    public int colorlimit;
	// Use this for initialization
	void Start () {

        countdown = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        TimeControl();
        TimeUI();
        LastMinuteRed();

        Overtime();
    }

    public void TimeControl()
    {
        if (second <= 0)
        {
            second = 59;
            if (timelimit >= 1)
            {
                --timelimit;
            }
            else
            {
                timelimit = 0;
                second = 0;
            }

        }
        else
        {
            second -= Time.deltaTime;
        }

        
    }

    public void TimeUI()
    {
        countdown.text = timelimit.ToString("00") + ":" + second.ToString("00");
    }
    public void LastMinuteRed()
    {
        if(timelimit <= colorlimit)
        {
            countdown.color = Color.red;
        }
    }

    public void Overtime()
    {
        if(timelimit <= 0)
            AutoFade.LoadLevel(3, .3f, .3f, Palette.DARK_PURPLE);
    }
}
