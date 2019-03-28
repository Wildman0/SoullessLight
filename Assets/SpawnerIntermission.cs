using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dreamteck.Splines;

public class SpawnerIntermission : MonoBehaviour
{
    private SplineComputer splineC;
    public float spawnDelay = 1f;
    public int spawnAmount = 0;
    public int maxSpawnAmount = 10;
    public string id = "orb";
    public float speed = 5f;
    private Transform spTransform;
    private float timer = 0;

    private void Awake()
    {
        maxSpawnAmount--;
            splineC = GetComponent<SplineComputer>();
            spTransform = transform;
            Spawn();
    }


    private void Update()
    {
        if (spawnAmount >= maxSpawnAmount)
            return;

        if (timer < spawnDelay)
            timer += Time.deltaTime;
        else if (timer >= spawnDelay)
        {
            timer = 0;
            Spawn();
        }
    }

    private void Spawn()
    {
        GameObject clone = ObjectPoolManager.instance.CallObject(id, null, spTransform.position, spTransform.rotation);

        SplineFollower follower = clone.GetComponent<SplineFollower>();
        if (follower != null)
        {
            follower.followSpeed = speed;
            follower.computer = splineC;
        }
        spawnAmount++;
    }
}

