using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Chara_Selection : MonoBehaviour {

    //index
    //list de gameobject et non cat pour avoir accès a la methode setactive oklm
    private int mesh_i = 0;
	[HideInInspector]
    public int cats_i = 0;
    private float camX;
    private float camZ;

    private List<SkinnedMeshRenderer> cats_mesh = new List<SkinnedMeshRenderer>();
    public List<GameObject> cats = new List<GameObject>();
    public Camera cam;

    //loadscene
    public string nextScene = "Multi - Jardin";

    void Start () {
        //on fait disparaitre tous les models sauf le premier
        for(int i = 0; i < cats.Count; ++i)
        {
            cats_mesh.Add(cats[i].GetComponentInChildren<SkinnedMeshRenderer>());
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
        cats_mesh[mesh_i].enabled = false;

        //check input for swaping chara
        if (Input.GetButtonDown("left"))
        {
            --mesh_i;
            --cats_i;
            UpdateCam(false);

            if (mesh_i < 0)
            {
                mesh_i = cats_mesh.Count - 1;
                cats_i = mesh_i;
                cam.transform.position = new Vector3(2.98f, 0, 0.68f);
            }
            
        }

        if (Input.GetButtonDown("right"))
        {
            ++mesh_i;
            ++cats_i;
            UpdateCam(true);

            if (mesh_i >= cats_mesh.Count)
            {
                mesh_i = 0;
                cats_i = 0;
                cam.transform.position = new Vector3(3.98f,0,1.28f);
            }
            
        }

        //set active chara depending on user input
        cats_mesh[mesh_i].enabled = true;
    }

    void SaveChara()
    {
        //save chara the player choose for next scene 
        //next scene load
        if (Input.GetKeyDown(KeyCode.Return))
        {
            PlayerPrefs.SetInt("ChoosenCat", cats_i);
            AutoFade.LoadLevel(19, .3f, .3f, Palette.DARK_PURPLE);//9
        }

    }

    private void RotateRestrain()
    {
        if(Input.GetMouseButton(0))
            cats[cats_i].transform.Rotate(new Vector3(0.0f, Input.GetAxis("Mouse X"), 0.0f));
    }

    private void UpdateCam(bool direction)
    {
        //vers la droite //true
        if(direction)
        {
            camX = cam.GetComponent<Transform>().position.x - 0.35f;
            camZ = cam.GetComponent<Transform>().position.z - 0.20f;

            
        }
        
        //vers la gauche // false
        else
        {
            camX = cam.GetComponent<Transform>().position.x + 0.35f;
            camZ = cam.GetComponent<Transform>().position.z + 0.20f;
        }

        cam.transform.position = new Vector3(camX, 0.0f, camZ);
    }


    

    

}
