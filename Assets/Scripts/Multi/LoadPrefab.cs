using UnityEngine;
using System.Collections;

public class LoadPrefab : MonoBehaviour {

    public Object[] cat_prefabs;
    private int prefab;
	// Use this for initialization
	void Start () {

        cat_prefabs = Resources.LoadAll("Assets/Resources/Multi_Prefab");
        prefab = PlayerPrefs.GetInt("ChoosenCat");
        //Instantiate(cat_prefabs[prefab]);
        print("machin" + cat_prefabs.Length);
        foreach(Object obj in cat_prefabs)
        {
            print("coucou"+ obj.name);
        }
        print("super");
	}
	
}
