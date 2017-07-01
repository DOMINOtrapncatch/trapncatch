using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Swap : MonoBehaviour
{
	//coucou c'est amandine mdr si je fais des modifs je laisse tes anciennes ligne de code en commentaire tkt
	/* 
     * ---------
     * SwapManager by Julien
     * This script needs a camera for each cats
     * and tie this script to whatever you want
     * --------- 
     */

	// bind a cam, a cat and a key
	public List<Cat> cats;
    public List<Camera> cameras;

    //public List<KeyCode> keys;
    public string key;
    public HUDManager hud;

    [HideInInspector]
    public static bool HasSwapped { get; private set; }

    static int i = 0;

    void Start()
    {
        foreach (Cat cat in cats)
			cat.isPathfindingActive = true;

        // Starting by only activating the first camera
        SwapWith(0);
        HasSwapped = false;    
    }

    void Update()
    {
        if (Input.GetButtonDown(InputManager.Get(key)))
        {
            i = (i + 1) % cameras.Count;
            SwapWith(i);
        }
    }

    void SwapWith(int id)
	{
        for (int i = 0; i < cameras.Count; ++i)
		{
			cats[i].isPathfindingActive = i != id;
            cameras[i].enabled = i == id;
            cats[i].GetComponent<MoveThirdPerson>().enabled = i == id;
            cats[i].GetComponent<Animator>().enabled = i == id;
        }
        hud.LoadPlayer(cats[id]);
        HasSwapped = true;
    }

}
