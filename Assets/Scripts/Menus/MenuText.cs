using UnityEngine.UI;
using UnityEngine;

public class MenuText : MonoBehaviour
{
    public string stringMenuKey;

    void Start()
    {
        GetComponent<Text>().text = LanguageManager.GetMenuText(stringMenuKey).Replace("\\n", "\n");
    }
}
