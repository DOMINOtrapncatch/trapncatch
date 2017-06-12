using UnityEngine;
using UnityEngine.UI;

public class ChangeVolume : MonoBehaviour
{
    public Slider slideVolume;

    void Start()
    {
        slideVolume.value = AudioListener.volume;
    }

    public void SetVolume()
    {
        AudioListener.volume = slideVolume.value;
    }
}
