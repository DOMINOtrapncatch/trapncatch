﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Net;
using System.Collections;

public class Cat : Character
{
	// Variables qui pourront etres modifiees par l'utilisateur
	[Range(0, 100)]
	public float Mana;
	[Range(0, 100)]
	public float MaxMana, ManaMaxRecoveryTime;

	// Variables qui ne pourront pas etre modifiees par l'utilisateur
	[HideInInspector]
	public float ManaRecoveryTime = 0;

	// Valeur maximales brutes
	private float maxManaVal = 500, manaMaxRecoveryTimeVal = 40;

	// Variables utilisees dans les scripts
	public float mana                { get { return Mana                * maxMana                / 100; } }
	public float maxMana             { get { return MaxMana             * maxManaVal             / 100; } }
	public float manaMaxRecoveryTime { get { return ManaMaxRecoveryTime * manaMaxRecoveryTimeVal / 100; } }

	public Sprite icon;
	public List<Spell> attacks;
	public List<Spell> spells;
	[HideInInspector]
	public List<GameObject> nearEnemy = new List<GameObject>();
	[HideInInspector]
	public List<GameObject> aroundEnemy = new List<GameObject>();
	[HideInInspector]
	public int enemyKillCount = 0;

	[Header("Pathfinding Settings")]
	public bool isPathfindingActive = false;
	public CatType catType = CatType.NORMAL;

	const float minPathUpdateTime = .2f;
	const float pathUpdateMoveThreshold = .5f;

	public List<Transform> targets = new List<Transform>();
	public bool randomTarget = true;
	public float turnSpeed = 3;
	public float turnDst = 5;
	public float stoppingDst = 10;

	int targetIndex;
	Path path;

	void Update()
	{
		CheckSpells ();
        if (Input.GetKeyDown(KeyCode.P))
            AutoFade.LoadLevel(1, .3f, .3f, Palette.DARK_PURPLE);
    }

	public void CheckSpells()
	{
		foreach(Spell attack in attacks)
		{
			if(attack.CanUse ())
			{
				attack.Activate ();
			}
		}

		foreach(Spell spell in spells)
		{
			if(spell.CanUse ())
			{
				spell.Activate ();
			}
		}
	}

	public void AttackEnemy(int enemyIndex)
	{
		// Get enemy
		Mouse enemy = nearEnemy[enemyIndex].GetComponent<Mouse>();

		// Remove life
		if(attack > 0)
			enemy.Life -= attack;

		// If dead, make it disappear
		if (enemy.life <= 0)
			KillEnemy(enemyIndex);
	}

	public void KillEnemy(int enemyIndex)
	{
		// Get enemy
		Mouse enemy = nearEnemy[enemyIndex].GetComponent<Mouse>();

		// Spawn particle effect on deth spot and destroy it after it was animated
		GameObject deathParticleInstance = (GameObject)Instantiate(enemy.deathPrefab, nearEnemy[enemyIndex].transform.position, Quaternion.identity);
		Destroy(deathParticleInstance, 1.0f);

		// Destroy corresponding object
		Destroy(nearEnemy[enemyIndex]);

		// Remove enemy from lists
		aroundEnemy.Remove(nearEnemy[enemyIndex]);
		aroundEnemy.Remove(nearEnemy[enemyIndex]);
		nearEnemy.Remove(nearEnemy[enemyIndex]);

		// Increment number of enemies killes
		++enemyKillCount;
	}

	/*
	 *  =================
	 *   | PATHFINDING |
	 *  ==================
	 */

	void Start()
	{
		// Update randomizer seed
		Random.InitState(System.DateTime.Now.Millisecond);

		// Init start and target based on random or not
		if(targets.Count > 0)
		{
			if (randomTarget)
			{
				targetIndex = Random.Range(1, targets.Count - 1);

				int startIndex;
				if (targets.Count > 1)
				{
					do
					{
						startIndex = Random.Range(1, targets.Count - 1);
					}
					while (startIndex == targetIndex);
				}
				else
				{
					startIndex = 0;
				}

				transform.position = targets[startIndex].position;
			}
			else
			{
				targetIndex = 1;
				transform.position = targets[0].position;
			}
		}

		StartCoroutine(UpdatePath());
	}

	public void OnPathFound(Vector3[] waypoints, bool pathSuccessful)
	{
		if (pathSuccessful)
		{
			// Get success path
			path = new Path(waypoints, transform.position, turnDst, stoppingDst);

			StopCoroutine("FollowPath"); // In case coroutine alwready exists
			StartCoroutine("FollowPath");
		}
	}

	IEnumerator UpdatePath()
	{
		// Handle level loading trouble maker
		if (Time.timeSinceLevelLoad< .3f)
			yield return new WaitForSeconds(.3f);

		while(true)
		{
			if(isPathfindingActive)
			{
				if (targets.Count > 0 && targetIndex > 0)
				{
					PathRequestManager.RequestPath(new PathRequest(transform.position, targets[targetIndex].position, OnPathFound));

					float sqrMoveThreshold = pathUpdateMoveThreshold * pathUpdateMoveThreshold;
					Vector3 targetPosOld = targets[targetIndex].position;

					while (isPathfindingActive)
					{
						yield return new WaitForSeconds(minPathUpdateTime);

						if (targetIndex< 0)
							break;

						// Update only if moved a certain dist (this is here for performance issues)
						if ((targets[targetIndex].position - targetPosOld).sqrMagnitude > sqrMoveThreshold)
						{
							PathRequestManager.RequestPath(new PathRequest(transform.position, targets[targetIndex].position, OnPathFound));
							targetPosOld = targets[targetIndex].position;
						}
					}
				}
				else if(aroundEnemy.Count > 0)
				{
					PathRequestManager.RequestPath(new PathRequest(transform.position, aroundEnemy[0].transform.position, OnPathFound));

					float sqrMoveThreshold = pathUpdateMoveThreshold * pathUpdateMoveThreshold;
					Vector3 targetPosOld = aroundEnemy[0].transform.position;

					while (isPathfindingActive)
					{
						yield return new WaitForSeconds(minPathUpdateTime);

						if (aroundEnemy.Count == 0)
							break;
						
						// Update only if moved a certain dist (this is here for performance issues)
						if ((aroundEnemy[0].transform.position - targetPosOld).sqrMagnitude > sqrMoveThreshold)
						{
							PathRequestManager.RequestPath(new PathRequest(transform.position, aroundEnemy[0].transform.position, OnPathFound));
							targetPosOld = aroundEnemy[0].transform.position;
						}
					}
				}
				else
				{
					DecideNextMove();
					yield return new WaitForSeconds(minPathUpdateTime);
				}	
			}
			else
			{
				yield return new WaitForSeconds(minPathUpdateTime);
			}
		}
	}

	IEnumerator FollowPath()
	{
		bool followingPath = true;
		int pathIndex = 0;
		transform.LookAt(path.lookPoints[0]);

		float speedPercent = 1;

		while (followingPath && isPathfindingActive)
		{
			Vector2 pos2D = new Vector2(transform.position.x, transform.position.z);
			while (path.turnBoundaries[pathIndex].HasCrossedLine(pos2D))
			{
				if (pathIndex == path.finishLineIndex)
				{
					followingPath = false;
					break;
				}
				else
				{
					pathIndex++;
				}
			}

			if (followingPath)
			{
				if (pathIndex >= path.slowDownIndex && stoppingDst > 0)
				{
					speedPercent = Mathf.Clamp01(path.turnBoundaries[path.finishLineIndex].DistanceFromPoint(pos2D) / stoppingDst);
					followingPath = !(speedPercent < 0.2f);
				}

				Quaternion targetRotation = Quaternion.LookRotation(path.lookPoints[pathIndex] - transform.position);
				transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * turnSpeed);
				transform.Translate(Vector3.forward * Time.deltaTime * speed * speedPercent, Space.Self);
			}

			// Priority to the nearest enemy
			if (targetIndex != -1 && catType == CatType.NORMAL && aroundEnemy.Count > 0)
			{
				targetIndex = -1;
				break;
			}

			yield return null; // Move to the next frame
		}

		DecideNextMove();
	}

	private void DecideNextMove()
	{
		// Decide of the next move
		switch (catType)
		{
			case CatType.NORMAL:
				// If there are enemies near me, follow it. Else, go somewere you know.
				if (aroundEnemy.Count > 0)
				{
					targetIndex = -1;
				}
				else
				{
					if (randomTarget)
					{
						int startIndex = 0;
						if (targets.Count > 1)
						{
							do
							{
								startIndex = Random.Range(0, targets.Count - 1);
							}
							while (startIndex == targetIndex);
						}
						targetIndex = startIndex;
					}
					else
					{
						targetIndex = 1;
					}
				}
				break;
		}
	}

	public void OnDrawGizmos()
	{
		if (path != null)
		{
			path.DrawWithGizmos();
		}
	}
}

public enum CatType
{
	NORMAL
}