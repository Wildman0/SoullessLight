using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dreamteck.Splines;

public class SpawnerIntermission : MonoBehaviour {

    public bool infiniteSpawn = false;

    public int pooledAmount = 20;

    public float speed = 5f;
    public float maxSpawnAmount = 10f;
    private float spawnAmount = 0f;
    public float fireTime = 1;
    public float fireDelay = 1;

    public GameObject toSpawn;
    List<GameObject> orbs;

    // sets the pool size and the object then creates pool. then starts firing whilst setting span delay and time.
    private void Start()
    {
        SplineFollower follower = toSpawn.GetComponent<SplineFollower>();
        follower.followSpeed = speed;
      
        orbs = new List<GameObject>();
        for (int i = 0; i < pooledAmount; i++)
        {
            GameObject obj = (GameObject)Instantiate(toSpawn);
            obj.SetActive(false);
            orbs.Add(obj);
        }

        InvokeRepeating("Fire", fireTime, fireDelay);

    }

    // removes orb from pool and disables it when maxSpawnAmount reached.
    void Fire()
    {
        if (!infiniteSpawn)
        {

            if (spawnAmount == maxSpawnAmount)
            {
                CancelInvoke("Fire");
            }
            else
            {
                spawnAmount += 1;
            }
        }
        for (int i = 0; i < orbs.Count; i++)
        {
            if (!orbs[i].activeInHierarchy)
            {
                orbs[i].transform.position = transform.position;
                orbs[i].transform.rotation = transform.rotation;
                orbs[i].SetActive(true);
                break;
            }                
        }
    }

}
