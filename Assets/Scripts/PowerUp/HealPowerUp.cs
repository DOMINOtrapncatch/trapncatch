using UnityEngine;
using System.Collections;

public class HealPowerUp : PowerUp
{
    public int healValue = 40;

    protected override void Activate(Character enemy)
    {
        enemy.Heal(healValue);
        Destroy();
    }
}
