﻿using UnityEngine;
using System.Collections;
using UnityEditor.VersionControl;
using System.Runtime.InteropServices;

public class AverageCat : Cat
{
	void Start()
	{
		spells.Add (new BasicSpell (this, KeyCode.E));

        /*maxLife = 100;
        defense = 75;
        attack = 75;
        speed = 75;
        mana = 60;*/
    }
}
