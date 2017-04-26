using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public class Mode2_Survival : MonoBehaviour {

    public int maxPoolSize = 5;//20
    public float delai = 10.0f;
    public Vector3 spawn_position = new Vector3(-6.0f, -1.0f, 13.0f);
    public Quaternion spawn_rot = new Quaternion(0.0f,180.0f,0.0f,0.0f);

    private GameObject[] player_list;
    private Object[] spawnable_mouse;
    private List<Cat> cat_list;
    private List<Mouse> mouse;

    private int DeathCounter;
    private int spawn_index;

    void Start()
    {
        player_list = GameObject.FindGameObjectsWithTag("Cat");
        spawnable_mouse = AssetDatabase.LoadAllAssetsAtPath("Assets/Models/Characters/Prefabs/Mouse/LVL3");
        
        foreach(GameObject player in player_list)
        {
            cat_list.Add(player.GetComponent<Cat>());
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
            //spawn de 3 souris toutes les 10 secondes
            StartCoroutine("SpawnInTime");
        }

    }

    #region premier temps
    void KillCat()
    {
        //check if any cat is dead - destroy
        foreach(Cat cat in cat_list)
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
        foreach(Mouse mice in mouse)
        {
          if(mice.life <= 0)
            {
                DespawnMouse(mice);
                SpawnMouse();
            }
        }
    }
    void DespawnMouse(Mouse mice)
    {
        ++DeathCounter;
        mouse.Remove(mice);
        Destroy(mice);
    }
    void SpawnMouse()
    {
        //spawn de souris meutriere
        //choix entre 3 prefabs - random
        spawn_index = Random.Range(0, spawnable_mouse.Length - 1);
        Instantiate(spawnable_mouse[spawn_index], spawn_position, spawn_rot);

        //keep trace for other purpose
        mouse.Add(((GameObject) spawnable_mouse[spawn_index]).GetComponent<Mouse>());
    }
    #endregion

    #region troisieme temps

    IEnumerator SpawnInTime()
    {
        SpawnMouse();
        yield return new WaitForSeconds(delai);
    }
    #endregion

}
