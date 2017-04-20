using UnityEngine;
using System.Collections;
using System;

public class CatFood : Spell {

    int restraint = 45; //secondes

	void Start () {

        manaCost = 20;
	}
	

    public override void Activate()
    {
        //get the other cat.position and freeze it
        //check with timer
        throw new NotImplementedException();
    }
}
