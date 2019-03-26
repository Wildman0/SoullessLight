using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dreamteck.Splines;

public class SpawnerIntermission : MonoBehaviour
{
    private SplineComputer splineC;
    public float spawnDelay = 1f;
    private WaitForSeconds waitDelay;
    public int spawnAmount = 0;
    public int maxSpawnAmount = 10;
    public string id = "orb";
    public float speed = 5f;
    public bool velocityOrb = false;
    public Vector3 pos;
    public Quaternion rot;


    private void Awake()
    {
        if (!velocityOrb)
        {
            splineC = GetComponent<SplineComputer>();
        }
    }

    private void Start()
    {
        waitDelay = new WaitForSeconds(spawnDelay);
        StartCoroutine(Spawn());

        if (velocityOrb)
        {
            pos = transform.position;
            rot = transform.rotation;
        }
        else
        {
            pos = Vector3.zero;
            rot = Quaternion.identity;
        }
    }


    IEnumerator Spawn()
    {
        while (spawnAmount < maxSpawnAmount)
        {
            yield return waitDelay;
            GameObject clone = ObjectPoolManager.instance.CallObject(id, transform, pos, rot);

            if (!velocityOrb)
            {
                SplineFollower follower = clone.GetComponent<SplineFollower>();
                follower.followSpeed = speed;
                follower.computer = splineC;
            }
            spawnAmount++;
        }
    }

}
