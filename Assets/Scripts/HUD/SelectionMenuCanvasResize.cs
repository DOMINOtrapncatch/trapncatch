using UnityEngine;
using System.Collections;

public class SelectionMenuCanvasResize : MonoBehaviour {

	void Awake ()
	{
		// TODO - Resize canvas based on screen size (width variable and Canvas Scale Factor variable)

		RectTransform canvasContent = GetComponentInChildren<RectTransform>();
	}
}
