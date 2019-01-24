using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dreamteck.Splines;

public class Timed_Spawner : MonoBehaviour {

    public GameObject projectile;
    public bool stopSpawning = false;
    public bool repeatingSpawn = true;
    public float projectileSpeed = 20f;
    public float spawnTime;
    public float spawnDelay;
    public float lineLength = 10f;
    public float maxSpawnAmount = 100f;
    public float spawnAmount = 0f;
    public SplineComputer sc;



	void Start ()
    {
        InvokeRepeating("SpawnObject", spawnTime, spawnDelay);
    }
    
    public void SpawnObject()
    {
        GameObject projectile_01 = Instantiate(projectile, transform.position, transform.rotation) as GameObject;
        SplineFollower follower = projectile_01.GetComponent<SplineFollower>();


        if(repeatingSpawn == false)
        {
            spawnAmount = spawnAmount + 1;
            Debug.Log("Added 1");
        }
        
        
        if (follower != null)
        {
            follower.computer = sc;
        }

        Rigidbody rb = projectile_01.GetComponent<Rigidbody>();
        rb.velocity = transform.forward * projectileSpeed;
        
      
    }

    private void Update()
    {

        if (stopSpawning == true)
        {
            CancelInvoke("SpawnObject");
            //Debug.Log("Spawning Has Ended");
            Destroy(this.gameObject);
        }

        if (spawnAmount == maxSpawnAmount)
        {
            CancelInvoke("SpawnObject");
            //Debug.Log("Max Amount Reached");
            Destroy(this.gameObject);
        }

        Vector3 forward = transform.TransformDirection(Vector3.forward) * lineLength;
        Debug.DrawLine(transform.position, forward, Color.blue);
    }

}
