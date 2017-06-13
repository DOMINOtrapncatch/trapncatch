using UnityEngine;
using UnityEngine.Networking;

public class MManaBlowSpell : MSpell
{
	[Header("Spell Data")]

	public GameObject manaBlowParticlePrefab;
	[Range(0, 100)]
	public int lifeCostAmount = 20;
	public int manaLostAmount = 20;

	public override void Activate()
	{
        if (!isLocalPlayer)
            return;
		GameObject manaBlowParticleInstance = (GameObject)Instantiate(manaBlowParticlePrefab, cat.transform.position, Quaternion.identity);
        NetworkServer.Spawn(manaBlowParticleInstance);

		foreach(GameObject enemy in cat.aroundEnemy)
		{
			MCat enemyCat = enemy.GetComponent<MCat>();

			if (enemyCat != null)
			{
				float newMana = enemyCat.Mana - manaLostAmount;
				enemyCat.Mana = (newMana < 0) ? 0 : newMana;
			}
		}

		float newLife = cat.Life - lifeCostAmount;
		cat.Life  = (newLife < 0) ? 0 : newLife;

		Destroy(manaBlowParticleInstance, 3.0f);
	}
}