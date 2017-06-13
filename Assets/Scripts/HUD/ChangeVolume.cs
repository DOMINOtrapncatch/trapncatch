using System;
using UnityEngine;
using UnityEngine.UI;

public class ChangeVolume : MonoBehaviour
{
    public Slider slideVolume;

    void Start()
    {
        AudioListener.volume = float.Parse(SaveManager.Get("volume"));
        slideVolume.value = AudioListener.volume;
    }

    public void SetVolume()
    {
        AudioListener.volume = slideVolume.value;
        SaveManager.Set("volume", slideVolume.value.ToString());
    }
}
