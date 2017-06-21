using System.Collections;
using UnityEngine;

public class ExplosionAttack : Attack
{
    public int damageValue = 8;
    public GameObject explosionPrefab;

    Mouse mouse;

    public void Awake()
    {
        mouse = gameObject.GetComponent<Mouse>();
    }

    public override bool CanUse()
	{
        if (mouse.mouseType == MouseType.SUICIDE && (mouse.nearCats.Count > 0 || mouse.lifeSpanSeconds >= 8))
            return true;
        
        return base.CanUse();
    }

    public override void Activate()
	{
        // Apply damage to nearby cats
        for (int i = 0; i < mouse.nearCats.Count; i++)
		{
			mouse.nearCats[i].RemoveNearEnemy(mouse);
			mouse.nearCats[i].RemoveAroundEnemy(mouse);
            mouse.nearCats[i].Damage(gameObject, damageValue);
        }

		// Handle animation
		GameObject explosionEffect = (GameObject)Instantiate(explosionPrefab, transform.position, explosionPrefab.transform.rotation);
        mouse.Death(null);
	}
}