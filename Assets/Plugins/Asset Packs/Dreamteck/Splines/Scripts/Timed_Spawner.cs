using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dreamteck.Splines;

public class Timed_Spawner : MonoBehaviour {

    public GameObject projectile;
    public bool stopSpawning = false;
    public bool repeatingSpawn = true;
    public bool Looped = false;
    public float destroyAfterXTime = 5f;
    public float spawnTime;
    public float spawnDelay;
    public float maxSpawnAmount = 100f;
    public float spawnAmount = 0f;
    public float speed;
    public SplineComputer sc;



	void Start ()
    {
        InvokeRepeating("SpawnObject", spawnTime, spawnDelay);

    }
    
    public void SpawnObject()
    { 

        GameObject projectile1 = Instantiate(projectile, transform.position, transform.rotation) as GameObject;

        SplineFollower follower = projectile1.GetComponent<SplineFollower>();
        follower.followSpeed = speed;       

        if (Looped == true)
        {
            follower.wrapMode = SplineFollower.Wrap.Loop;
        }

        if (follower != null)
        {
            follower.computer = sc;
        }


        if (repeatingSpawn == false)
        {
            spawnAmount = spawnAmount + 1;
            //Debug.Log("Added 1");
        }
      
    }

    private void Update()
    {

        if (stopSpawning == true)
        {
            CancelInvoke("SpawnObject");
            //Debug.Log("Spawning Has Ended");
            Object.Destroy(this.gameObject, destroyAfterXTime);
        }

        if (spawnAmount == maxSpawnAmount)
        {
            CancelInvoke("SpawnObject");
            //Debug.Log("Max Amount Reached");
            Object.Destroy(this.gameObject, destroyAfterXTime);
        }

     
    }

}
