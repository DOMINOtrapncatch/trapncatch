using UnityEngine;

public class NearTrigger : MonoBehaviour
{
	Character player;

	void Start()
	{
		player = (Character)GetComponentInParent(typeof(Character));
	}

	void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.tag == "Character")
        {
            Character character = (Character)col.gameObject.GetComponentInParent(typeof(Character));
            if(character != player)
                player.AddNearEnemy(character);
        }
	}

	void OnTriggerExit(Collider col)
	{
		if (col.gameObject.tag == "Character")
            player.RemoveNearEnemy((Character)col.gameObject.GetComponentInParent(typeof(Character)));
	}
}
