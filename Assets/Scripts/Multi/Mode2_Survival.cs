using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Mode2_Survival : MonoBehaviour {

    /*
     * spawn 4 mouse meurtriere
     * when one dies -> spawn another
     * 
     * check playerlist
     * when one dies -> remove and load gameover scene for them
     * 
     * when timer = 0s
     * foreach remaining player -> load winning scene and score 
     */

    public int maxPoolSize = 5;//20
    public float delai = 10.0f;

    private GameObject[] player_list;
    public GameObject[] spawnable_mouse;
    private List<MCat> cat_list;
    private List<MMouse> mouse;

    private int DeathCounter;
    private int spawn_index;

    void Awake()
    {
        player_list = GameObject.FindGameObjectsWithTag("Cat");
        //spawnable_mouse = Resources.LoadAll("Assets/Models/Characters/Prefabs/Mouse/LVL3");
        spawnable_mouse = GameObject.FindGameObjectsWithTag("Enemy");
        
        for(int i = 0; i < player_list.Length;++i)
        {
            cat_list.Add(player_list[i].GetComponent<MCat>());
        }

        for (int i = 0; i < spawnable_mouse.Length; ++i)
        {
            mouse.Add(spawnable_mouse[i].GetComponent<MMouse>());
        }

        //Spawn de depart
        SpawnMouse();
        SpawnMouse();
    }

    void Update()
    {
        // ** premier temps **
        //check for gameover
        KillCat();
        Gameover();
        
        if(mouse.Count < maxPoolSize)
        {

            // ** deuxieme temps **
            //IF : une souris est morte
            // on despawn la morte pour en respawn une autre
            CheckForSpawn();

            // ** troisieme temps **
            //spawn de 3 souris toutes les 10 secondes -> non
            StartCoroutine("SpawnInTime");
        }

    }

    #region premier temps
    void KillCat()
    {
        //check if any cat is dead - destroy
        foreach(MCat cat in cat_list)
        {
            if(cat.Life <= 0)
            {
                Destroy(cat);
            }
        }
    }

    void Gameover()
    {
        //if there is only one cat left its the winner
        if(player_list.Length == 1)
        {
            //charge message/canvas (stop toute action sur le jeu) pour le winner avant de charger scene victoire
        }
    }
    #endregion

    #region deuxieme temps
    void CheckForSpawn()
    {
        foreach(MMouse mice in mouse)
        {
          if(mice.life <= 0)
            {
                DespawnMouse(mice);
                SpawnMouse();
            }
        }
    }
    void DespawnMouse(MMouse mice)
    {
        ++DeathCounter;
        mouse.Remove(mice);
        Destroy(mice);
    }
    void SpawnMouse()
    {
        print("spawn");
        //spawn de souris meutriere
        //choix entre 3 prefabs - random
        spawn_index = Random.Range(0, spawnable_mouse.Length - 1);
        Instantiate(spawnable_mouse[spawn_index], transform.position, Quaternion.identity);

        //keep trace for other purpose
        mouse.Add(((GameObject) spawnable_mouse[spawn_index]).GetComponent<MMouse>());
    }
    #endregion

    #region troisieme temps

    //non jetais bourree ou quoi
    IEnumerator SpawnInTime()
    {
        SpawnMouse();
        yield return new WaitForSeconds(delai);
    }
    #endregion

}
