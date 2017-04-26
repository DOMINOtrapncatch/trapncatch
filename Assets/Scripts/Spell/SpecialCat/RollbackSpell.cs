using UnityEngine;
using System.Collections.Generic;

public class RollbackSpell : Spell {

	public GameObject rollbackUpParticle;
	public GameObject rollbackDownParticle;

	CatSave[] catSaves = new CatSave[10];

	int counterCatSave = 0;

	public override void Activate()
	{
		StartCoroutine("Rollback");
	}

	public override bool CanUse()
	{
		if (catSaves[(counterCatSave + 1) % 10] == null)
			return false;
		else
			return base.CanUse();
	}

	public override void UpdateEverySecond()
	{
		catSaves[counterCatSave] = new CatSave(cat);
		counterCatSave = (counterCatSave + 1) % 10;
	}

	private void Rollback()
	{
		counterCatSave = (counterCatSave + 1) % 10;

		GameObject rollbackUpObject   = (GameObject)Instantiate(rollbackUpParticle  , cat.transform.position                        , Quaternion.identity);
		GameObject rollbackDownObject = (GameObject)Instantiate(rollbackDownParticle, catSaves[counterCatSave].position + Vector3.up, Quaternion.identity);

        Destroy(rollbackUpObject  , 1.0f);
        Destroy(rollbackDownObject, 1.0f);

		catSaves[counterCatSave].Rollback(cat);
	}

	private class CatSave
	{
		public Vector3 position { get; private set; }
		Quaternion rotation;
		float Life;
		float Mana;

		public CatSave(Cat cat)
		{
			position = cat.transform.position;
			rotation = cat.transform.rotation;
			Life = cat.Life;
			Mana = cat.Mana;
		}

		public void Rollback(Cat cat)
		{
			cat.transform.position = position;
			cat.transform.rotation = rotation;
			cat.Life = Life;
			cat.Mana = Mana;
		}
	}
}
