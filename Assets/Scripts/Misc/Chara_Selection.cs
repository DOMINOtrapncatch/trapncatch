using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Chara_Selection : MonoBehaviour {

    //index
    //list de gameobject et non cat pour avoir accès a la methode setactive oklm
    private int i = 0;
    public List<SkinnedMeshRenderer> cats_mesh = new List<SkinnedMeshRenderer>();


	void Start () {
        
        
        //on fait disparaitre tous les models sauf le premier
        for(int i = 1; i < cats_mesh.Count; ++i)
        {
            cats_mesh[i].enabled = false;
        }

	
	}
	
	
	void Update () {

        
        //rotate charac to check him how sexy it is
        //we might delete this since we swap mesh and not entire gameobject
        RotateRestrain();

        //use left/q or right/d for swaping chara
        SwapCharacter();
        SaveChara(); 
	}

    void SwapCharacter ()
    {
        //prevent overlapping
        cats_mesh[i].enabled = false;

        //check input for swaping chara
        if (Input.GetButtonDown("left"))
        {
            --i;

            if (i < 0)
                i = cats_mesh.Count - 1;
            
        }

        if (Input.GetButtonDown("right"))
        {
            ++i;

            if (i >= cats_mesh.Count)
                i = 0;
            
        }

        //set active chara depending on user input

        cats_mesh[i].enabled = true;
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

   /* void EnableAnimation()
    {
        cats[i].GetComponent<Animation>().enabled = true;
        bool isitt = cats[i].GetComponent<Animator>().isActiveAndEnabled;
        print(isitt);
    }*/

}
