using UnityEngine;

public class PowerUp : MonoBehaviour
{
	void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.tag == "Cat")
            Activate((Character)col.gameObject.GetComponent(typeof(Character)));
	}

	protected virtual void Activate(Character enemy){}
}
