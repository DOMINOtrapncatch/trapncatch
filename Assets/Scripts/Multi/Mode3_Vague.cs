using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Collections.Generic;

public class Mode3_Vague : NetworkBehaviour {

    // timer -> toutes les minutes on spawn 2 souris de la meme categorie au hasard parmi toutes les categories disponibles
    // player avec le meilleur score gagne
    //add syncvarlist machin
    private Object[] mice_ressource;
    private GameObject[] player_list;
    private List<MCat> cats;
    private int rand;
    public Unet_Timer timer;

    void Start()
    {
        player_list = GameObject.FindGameObjectsWithTag("Cat");
        for(int i = 0; i < player_list.Length;++i)
        {
            cats.Add(player_list[i].GetComponent<MCat>());
        }

        //get all mouse models
    }

    void Update()
    {

    }

    //spawn manager unet ou hardcode
    //tel es tla qursiotn je fais e que j veux
    public void MouseRandomSpawn()
    {
        if(timer.timer % 2 == 0)
        {
            rand = Random.Range(1, 3);
            //instantiate from mice_ressource (2 times)
        }
    }
}
