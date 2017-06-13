using UnityEngine;
using System.Collections;

public class SpeedPowerUp : PowerUp
{
    public float speedIncrease = 10.0f;
    public float speedTimeout = 15.0f;

    protected override void Activate(Character enemy)
    {
		enemy.StartCoroutine(Speed(enemy));
		Destroy();
    }

    private IEnumerator Speed(Character enemy)
    {
        gameObject.SetActive(false);

        enemy.Speed += speedIncrease;
        yield return new WaitForSeconds(speedTimeout);
        enemy.Speed -= speedIncrease;
    }
}
