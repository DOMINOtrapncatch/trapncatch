using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Mission1Dialogue : MonoBehaviour
{
    public Text Str;
    string Txt;
    int clicks;

    // Use this for initialization
    void Start()
    {
        Str = GameObject.Find("DialogueText").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        Str.text = Txt;

        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
            Next();
        if (clicks == 0)
            Txt = "Ah une souris,";
        if (clicks == 1)
            Txt = "J'espère que celle ci pourra me divertir.";
    }

    public void Next()
    {
        clicks++;
    }
}
