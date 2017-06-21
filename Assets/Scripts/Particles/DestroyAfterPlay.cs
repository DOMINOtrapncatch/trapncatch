using UnityEngine;

public class DestroyAfterPlay : MonoBehaviour
{
	ParticleSystem ps;

	public void Awake()
	{
		ps = GetComponent<ParticleSystem>();
	}

	public void Update()
	{
		if (ps && !ps.IsAlive())
			Destroy(gameObject);
	}
}
