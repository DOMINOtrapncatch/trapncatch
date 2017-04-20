using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Chara_Selection : MonoBehaviour {

    //index
    //list de gameobject et non cat pour avoir accès a la methode setactive oklm
    private int i = 0;
    public List<GameObject> cats = new List<GameObject>();

	void Start () {

        //on fait disparaitre tous les models sauf le premier
        for(int i = 1; i < cats.Count; ++i)
        {
            cats[i].SetActive(false);
        }
	
	}
	
	
	void Update () {
        print(i);
        //rotate charac to check him how sexy it is
        RotateRestrain();

        //use left/q or right/d for swaping chara
        SwapCharacter();
        SaveChara(); 
	}

    void SwapCharacter ()
    {
        //prevent overlapping
        cats[i].SetActive(false);

        //check input for swaping chara
        if (Input.GetButtonDown("left"))
        {
            --i;

            if (i < 0)
                i = cats.Count - 1;
            
        }

        if (Input.GetButtonDown("right"))
        {
            ++i;

            if (i >= cats.Count)
                i = 0;
            
        }

        //set active chara depending on user input
        cats[i].SetActive(true);
    }

    void SaveChara()
    {
        //save chara the player choose for next scene 
        //next scene load

        if(Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            PlayerPrefs.SetInt("ChoosenCat", i);
            AutoFade.LoadLevel(1, .3f, .3f, Palette.DARK_PURPLE);
        }
    }

    void RotateRestrain()
    {
        if(Input.GetMouseButton(0))
            transform.Rotate(new Vector3(0.0f, Input.GetAxis("Mouse X"), 0.0f));
    }
}
