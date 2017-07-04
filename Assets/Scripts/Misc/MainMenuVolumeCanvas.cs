using UnityEngine;
using System.Collections;

public class MainMenuVolumeCanvas : MonoBehaviour {

    //dsl mais je prefere rajouter un script plutot que de casser une bonne architecture x)
    //pas trop le temps de faire gaffe genre tmtc

    //ceci est un script offrant plein de possibilité genre publicité et tout ça et genre oe et genre la vie cest des choxu a al mdoe de chez onsou

    private GameObject canvas;
    private bool retry;

    void Start()
    {
        canvas = GameObject.Find("PauseMenu");
        canvas.SetActive(true);
        retry = false;
    }

    void Update () {

        DeletePopCanvas();
	}

    private void DeletePopCanvas()
    {
        if (!retry)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                canvas.SetActive(false);
                retry = true;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                canvas.SetActive(true);
                retry = false;
            }
        }

        //askip si il a changé d'avis

    }
}
