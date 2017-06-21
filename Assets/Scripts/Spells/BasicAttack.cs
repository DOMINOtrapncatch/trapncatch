public class BasicAttack : Attack
{
    public override void Activate()
	{
        for (int i = 0; i < player.nearEnemy.Count; i++)
            player.nearEnemy[i].Damage(player.gameObject, player.Attack);
    }
}
