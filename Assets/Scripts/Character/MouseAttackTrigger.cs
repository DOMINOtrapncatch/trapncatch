using UnityEngine;

public class MouseAttackTrigger : MonoBehaviour {

	Mouse mouse;

	void Start()
	{
		mouse = GetComponentInParent<Mouse>();
	}

	void OnTriggerEnter(Collider col)
	{
        if (col.gameObject.tag == "Cat")
			mouse.TryExplodeOn(col.gameObject.GetComponent<Cat>());
	}
}
