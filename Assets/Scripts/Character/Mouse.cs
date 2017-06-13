﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Mouse : Character
{
    public int damageValue = 10;

    [Header("Pathfinding Settings")]
    public MouseIA mouseIA = MouseIA.NORMAL;
    public MouseType mouseType = MouseType.LVL1;

    const float minPathUpdateTime = .2f;
    const float pathUpdateMoveThreshold = .5f;

	public List<Transform> targets = new List<Transform>();
    public bool randomTarget = true;
    public float turnSpeed = 3;
    public float turnDst = 5;
    public float stoppingDst = 10;

    int targetIndex;
    [HideInInspector]
    public int hideIndex = -1;
    Path path;

    [Header("GUI Settings")]
    public Image healthBar;

    [Header("Particle Effects")]
    public GameObject deathPrefab;
	public GameObject explosionPrefab;

	[HideInInspector]
	public List<GameObject> aroundCats = new List<GameObject>();

    void Start()
    {
        // Update randomizer seed
        Random.InitState(System.DateTime.Now.Millisecond);

        // Init start and target based on random or not
        if (randomTarget && mouseIA != MouseIA.SUICIDE)
        {
            targetIndex = Random.Range(0, targets.Count - 1);

            int startIndex;
            if (targets.Count > 1)
            {
                do
                {
                    startIndex = Random.Range(0, targets.Count - 1);
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

        StartCoroutine(UpdatePath());
    }

    void Update()
    {
        healthBar.fillAmount = this.life / this.maxLife;
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
        if (Time.timeSinceLevelLoad < .3f)
            yield return new WaitForSeconds(.3f);

        PathRequestManager.RequestPath(new PathRequest(transform.position, targets[targetIndex].position, OnPathFound));

        float sqrMoveThreshold = pathUpdateMoveThreshold * pathUpdateMoveThreshold;
        Vector3 targetPosOld = targets[targetIndex].position;

        while (true)
		{
            try
            {
                if (hideIndex >= 0)
                {
                    if ((targets[hideIndex].position - targetPosOld).sqrMagnitude > sqrMoveThreshold)
                    {
                        PathRequestManager.RequestPath(new PathRequest(transform.position, targets[hideIndex].position, OnPathFound));
                        targetPosOld = targets[hideIndex].position;
                    }
                    else if (aroundCats.Count == 0)
                    {
                        targetIndex = (targetIndex + 1) % targets.Count;
                        if (targetIndex == hideIndex)
                            targetIndex = (targetIndex + 1) % targets.Count;
                        hideIndex = -1;
                    }
                    else
                    {
                        hideIndex = GetBestSpotToGetAwayFromCat();
                    }
                }
                else
                {
                    if ((targets[targetIndex].position - targetPosOld).sqrMagnitude > sqrMoveThreshold)
                    {
                        PathRequestManager.RequestPath(new PathRequest(transform.position, targets[targetIndex].position, OnPathFound));
                        targetPosOld = targets[targetIndex].position;
                    }
                }
            }

            catch
            {
                this.Destroy();
            }

			yield return new WaitForSeconds(minPathUpdateTime);
        }
    }

    IEnumerator FollowPath()
    {
        bool followingPath = true;
        int pathIndex = 0;
        transform.LookAt(path.lookPoints[0]);

		float speedPercent = 1;

        while (followingPath)
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
            if (mouseIA == MouseIA.SMART && aroundCats.Count > 0)
            {
                hideIndex = GetBestSpotToGetAwayFromCat();

				if (hideIndex < 0)
                {
					break;
                }
            }
			// Get out if enemy is away
			if (hideIndex >= 0 && mouseIA == MouseIA.SMART && aroundCats.Count == 0)
			{
                hideIndex = -1;
				break;
			}
            
            yield return null; // Move to the next frame
        }

        // Decide of the next move
        switch (mouseIA)
        {
            case MouseIA.SUICIDE:
                // Keep following
                targetIndex = randomTarget ? Random.Range(1, targets.Count - 1) : 1;
                break;

            case MouseIA.CIRCLE:
                // Go to next point
                targetIndex = (targetIndex + 1) % targets.Count;
				break;

            case MouseIA.SMART:
                // Go to next point if there ane no ennemies around
                if(aroundCats.Count == 0)
				{
					targetIndex = (targetIndex + 1) % targets.Count;
                    hideIndex = -1;
                }
                else if(!followingPath && aroundCats.Count > 0)
                {
                    hideIndex = GetBestSpotToGetAwayFromCat();
                }
                break;

            case MouseIA.NORMAL:
                // Disappear
                Destroy(gameObject);
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

    public void TryExplodeOn(Cat enemy)
    {
        if (mouseIA == MouseIA.SUICIDE && enemy != null)
        {
            enemy.StartCoroutine(ExplodeOn(enemy));
            enemy.KillEnemy(gameObject);
        }
    }

    IEnumerator ExplodeOn(Cat enemy)
    {
        //Deal damage to enemy
        enemy.Damage(damageValue);

        // Handle animation
        GameObject explosionEffect = (GameObject)Instantiate(explosionPrefab, transform.position, explosionPrefab.transform.rotation);
        yield return new WaitForSeconds(2.0f);
        Destroy(explosionEffect);
    }

    int GetBestSpotToGetAwayFromCat()
    {
        int bestSpot = 0;
        float bestValue = Vector3.Distance(targets[0].position, aroundCats[0].transform.position) - Vector3.Distance(targets[0].position, transform.position);

        for (int i = 1; i < targets.Count; i++)
        {
			float newValue = Vector3.Distance(targets[i].position, aroundCats[0].transform.position) - Vector3.Distance(targets[i].position, transform.position);
			if (newValue > bestValue)
			{
				bestSpot = i;
				bestValue = newValue;
			}
        }

        return bestSpot;
    }

}

public enum MouseIA
{
    NORMAL, SUICIDE, CIRCLE, SMART
}

public enum MouseType
{
    LVL1, LVL2, LVL3
}