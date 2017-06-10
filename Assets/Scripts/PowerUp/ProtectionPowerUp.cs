using UnityEngine;
using System.Collections;

public class ProtectionPowerUp : PowerUp
{
	public float protectionIncrease = 10.0f;
	public float protectionTimeout = 15.0f;

	protected override void Activate(Character enemy)
	{
		enemy.StartCoroutine(Protection(enemy));
	}

	private IEnumerator Protection(Character enemy)
	{
		gameObject.SetActive(false);

		enemy.Attack += protectionIncrease;
		yield return new WaitForSeconds(protectionTimeout);
		enemy.Attack -= protectionIncrease;

		Destroy(gameObject);
	}
}
