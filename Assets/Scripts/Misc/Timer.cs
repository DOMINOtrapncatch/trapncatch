using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

    public Text countdown;
    public float timelimit;//minute
    public float second;
	// Use this for initialization
	void Start () {

        countdown = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        TimeControl();
        LastMinuteRed();
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

        countdown.text = timelimit.ToString("00") + ":" + second.ToString("00");
    }

    
    public void LastMinuteRed()
    {
        if(timelimit <= 3)
        {
            countdown.color = Color.red;
        }
    }
}
