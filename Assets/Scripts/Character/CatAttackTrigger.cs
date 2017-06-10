using UnityEngine;
using System.Collections;

public class CatAttackTrigger : MonoBehaviour {

	Cat cat;

	void Start()
	{
		cat = GetComponentInParent<Cat>();
	}

	void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.tag == "Enemy")
		{
			cat.nearEnemy.Add(col.gameObject.transform.parent.gameObject);
			if (cat.isPathfindingActive)
				cat.AttackEnemy(0);
		}
		else if (col.gameObject.tag == "Collider")
			cat.nearColliders.Add(col.gameObject.transform.parent.gameObject);
	}

	void OnTriggerExit(Collider col)
	{
		if (col.gameObject.tag == "Enemy")
			cat.nearEnemy.Remove(col.gameObject.transform.parent.gameObject);
	}
}
