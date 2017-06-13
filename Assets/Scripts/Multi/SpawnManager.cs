using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class SpawnManager : NetworkBehaviour {

    public GameObject cat;
    public GameObject[] cat_list;
    public int poolSize = 4;

    public NetworkHash128 assetID { get; set; }

    public delegate GameObject SpawnDelegate(Vector3 pos, NetworkHash128 assetID);
    public delegate void UnSpawnDelegate(GameObject spawned);

    void Start()
    {
        assetID = cat.GetComponent<NetworkIdentity>().assetId;
        cat_list = new GameObject[poolSize];

        for(int i = 0; i < poolSize; ++i)
        {
            cat_list[i] = (GameObject)Instantiate(cat, transform.position, Quaternion.identity);
            cat_list[i].name = cat.name + i;
            cat_list[i].SetActive(false);
        }

        ClientScene.RegisterSpawnHandler(assetID, SpawnObject, UnSpawnObject);
    }

    public GameObject GetFromPool(Vector3 pos)
    {
        foreach(GameObject kitty in cat_list)
        {
            if(!kitty.activeInHierarchy)
            {
                cat.transform.position = pos;
                cat.SetActive(true);
                return cat;
            }
        }

        return null;
    }

    public GameObject SpawnObject(Vector3 pos, NetworkHash128 assetID)
    {
        return GetFromPool(pos);
    }

    public void UnSpawnObject(GameObject spawned)
    {
        spawned.SetActive(false);
    }
}
