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
    public List<string> keys;

    void Start()
    {
        if (cameras.Count == 0 || cameras.Count != keys.Count)
            throw new Exception("Need at least one camera!");

        // Starting by only activating the first camera
        SwapWith(0);

        
    }

    void Update()
    {
        for (int i = 0; i < keys.Count; ++i)
        {
            if (Input.GetButtonDown(keys[i]))
                SwapWith(i);
        }
    }

    void SwapWith(int id)
    {
        for (int i = 0; i < cameras.Count; ++i)
        {
            cameras[i].enabled = i == id;
            cats[i].GetComponent<MoveThirdPerson>().enabled = i == id;
            cats[i].GetComponent<Animator>().enabled = i == id;
        }
    }

}
