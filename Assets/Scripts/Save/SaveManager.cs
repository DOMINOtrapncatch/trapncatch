using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System;

public class SaveManager : MonoBehaviour
{
    static Dictionary<string, string> data;
    static string file = "data.sav";

    void Start()
    {
        CreateSave();
        GetSave();
    }

    // Create the file if needed.
    private void CreateSave()
    {
        string missions = "mission0=0\nmission1=0\nmission2=0\nmission3=0\nmission4=0\nmission5=0\n";
        string spells = "spell 1=&\nspell 2=é\nspell 3=\"\nspell 4='\nattaque=MOUSE 0\nswap=MOUSE 1\n";
        string movements = "up=Z\nleft=Q\nright=D\ndown=S\nvolume=0.5\njump=Space";

        if (!File.Exists(file))
            File.WriteAllText(file, missions + spells + movements);
    }

    // Update data
    private void GetSave()
    {
        data = new Dictionary<string, string>();
        string[] lines = File.ReadAllText(file).Split('\n');

        for (int i = 0; i < lines.Length; ++i)
        {
            string[] lineValues = lines[i].Split('=');
            data.Add(lineValues[0], lineValues[1]);
        }
    }

    /// <summary>
    /// Save the key to the correct binding.
    /// </summary>
    /// <param name="input">For exemple : "spell1".</param>
    /// <param name="key">The key you want to save as the new one.</param>
    public static void Set(string input, string key)
    {
        data[input] = key;

        string newData = "";
        int i = 0;

        foreach (var d in data)
        {
            newData += d.Key + "=" + d.Value;
            if (i != data.Count - 1)
                newData += "\n";
            ++i;
        }

        File.WriteAllText(file, newData);
    }

    /// <summary>
    /// Get the key associated with the correct input.
    /// </summary>
    /// <param name="input">The input you want.</param>
    /// <returns>Key in string</returns>
    public static string Get(string input)
    {
        return data[input];
    }
}
