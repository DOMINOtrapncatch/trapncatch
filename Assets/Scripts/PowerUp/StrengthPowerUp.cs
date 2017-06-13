using UnityEngine;
using System.Collections;

public class StrengthPowerUp : PowerUp
{
    public float strengthIncrease = 10.0f;
    public float strengthTimeout = 15.0f;

    protected override void Activate(Character enemy)
    {
        enemy.StartCoroutine(Strength(enemy));
        Destroy();
    }

    private IEnumerator Strength(Character enemy)
    {
        gameObject.SetActive(false);

        enemy.Attack += strengthIncrease;
        yield return new WaitForSeconds(strengthTimeout);
        enemy.Attack -= strengthIncrease;
    }
}
