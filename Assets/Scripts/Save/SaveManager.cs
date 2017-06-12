using UnityEngine;
using System.Collections;
using System.IO;
using System;

public class SaveManager : MonoBehaviour
{
    static string mission;
    static string file = "mission.sav";
    
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
        mission = File.ReadAllText(file);
    }

    /// <summary>
    /// Check if mission is completed.
    /// </summary>
    /// <param name="id">id of the mission to be checked.</param>
    /// <returns></returns>
    public static bool Completed(int id)
    {
        return mission[id] == '1';
    }

    // Set a mission state to completed
    private static void SetMission(int id)
    {
        string newData = "";

        for (int i = 0; i < mission.Length; ++i)
        {
            if (id == i)
                newData += '1';
            else
                newData += mission[i];
        }

        mission = newData;
    }

    /// <summary>
    /// Save the progression.
    /// <param name="id">id of the mission to change.</param>
    /// </summary>
    public static void SaveMission(int id)
    {
        SetMission(id);
        File.WriteAllText(file, mission);
    }

    /// <summary>
    /// Save the key to the correct binding.
    /// </summary>
    /// <param name="input">For exemple : "spell1".</param>
    /// <param name="key">The key you want to save as the new one.</param>
    public static void SetKey(string input, string key)
    {

    }

    /// <summary>
    /// Get the key associated with the correct input.
    /// </summary>
    /// <param name="input">The input you want.</param>
    /// <returns>Key in string</returns>
    public static string GetKey(string input)
    {
        return "";
    }
}
