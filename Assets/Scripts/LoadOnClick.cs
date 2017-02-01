using UnityEngine;
using System.Collections;

public class LoadOnClick : MonoBehaviour
{
    public void LoadButton(int button)
    {
        Application.LoadLevel(button);
    }
}
