using UnityEngine;
using System.Collections;

public class CatProximityTrigger : MonoBehaviour {

	Cat cat;

	void Start()
	{
		cat = GetComponentInParent<Cat>();
	}

	void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.tag == "Enemy")
			cat.aroundEnemy.Add(col.gameObject.transform.parent.gameObject);
	}

	void OnTriggerExit(Collider col)
	{
		if (col.gameObject.tag == "Enemy")
			cat.aroundEnemy.Remove(col.gameObject.transform.parent.gameObject);
	}
}
