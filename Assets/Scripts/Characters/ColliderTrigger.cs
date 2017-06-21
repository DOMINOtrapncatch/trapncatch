using UnityEngine;

public class ColliderTrigger : MonoBehaviour
{
	Character player;

	void Start()
	{
        player = (Character)GetComponentInParent(typeof(Character));
	}

	void OnTriggerEnter(Collider col)
	{
        if (col.gameObject.tag == "Collider")
            player.AddCollider(col.gameObject);
	}

	void OnTriggerExit(Collider col)
	{
		if (col.gameObject.tag == "Collider")
            player.RemoveCollider(col.gameObject);
	}
}
