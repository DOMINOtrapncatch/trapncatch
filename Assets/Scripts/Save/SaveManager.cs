using UnityEngine;
using System.Collections;
using System.IO;
using System;

public class SaveManager : MonoBehaviour
{
    static string data;
    static string file = "data.sav";
    
    void Start()
    {
        CreateSave();
        GetSave();
    }

    // Create the file if needed.
    private void CreateSave()
    {
        if (!File.Exists(file))
            File.WriteAllText(file, "000000");
    }

    // Update data
    private void GetSave()
    {
        data = File.ReadAllText(file);
    }

    /// <summary>
    /// Check if mission is completed.
    /// </summary>
    /// <param name="id">id of the mission to be checked.</param>
    /// <returns></returns>
    public static bool Completed(int id)
    {
        return data[id] == '1';
    }

    // Set a mission state to completed
    private static void Set(int id)
    {
        string newData = "";

        for (int i = 0; i < data.Length; ++i)
        {
            if (id == i)
                newData += '1';
            else
                newData += data[i];
        }

        data = newData;
    }

    /// <summary>
    /// Save the progression.
    /// <param name="id">id of the mission to change.</param>
    /// </summary>
    public static void Save(int id)
    {
        Set(id);
        File.WriteAllText(file, data);
    }
}
