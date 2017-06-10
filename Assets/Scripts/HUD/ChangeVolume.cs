using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChangeVolume : MonoBehaviour {

    public Slider slideVolume;
    public AudioSource musicSource;

    public void SetVolume()
    {
        musicSource.volume = slideVolume.value;
    }
	
}
