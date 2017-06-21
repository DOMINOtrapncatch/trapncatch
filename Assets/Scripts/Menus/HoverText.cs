using UnityEngine;
using UnityEngine.UI;

public class HoverText : MonoBehaviour
{
    public int deltaSize = 10;
    Text text;

    void Start()
    {
        text = GetComponent<Text>();
    }

    public void Hover()
    {
        text.fontSize += deltaSize;
    }

	public void UnHover()
	{
        text.fontSize -= deltaSize;
	}
}
