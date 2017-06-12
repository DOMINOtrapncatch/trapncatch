using UnityEngine;

public class MouseProximityTrigger : MonoBehaviour {

	Mouse mouse;

	void Start()
	{
		mouse = GetComponentInParent<Mouse>();
	}

	void OnTriggerEnter(Collider col)
	{
        if (col.gameObject.tag == "Cat")
            mouse.aroundCats.Add(col.gameObject);
	}

	void OnTriggerExit(Collider col)
	{
		if (col.gameObject.tag == "Cat")
            mouse.aroundCats.Remove(col.gameObject);
	}
}
