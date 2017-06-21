using UnityEngine;

public class AroundTrigger : MonoBehaviour
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
			if (character != player)
                player.AddAroundEnemy(character);
        }
	}

	void OnTriggerExit(Collider col)
	{
        if (col.gameObject.tag == "Character")
            player.RemoveAroundEnemy((Character)col.gameObject.GetComponentInParent(typeof(Character)));
	}
}
