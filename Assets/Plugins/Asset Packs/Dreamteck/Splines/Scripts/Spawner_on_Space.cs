using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner_on_Space : MonoBehaviour {

    public GameObject projectile;
    public float projectileSpeed = 20;
    
	
	// This needs to be changed to a seperate function when importing to the main project it is only on void update and under space trigger for testing reasons
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject projectile_01 = Instantiate(projectile, transform) as GameObject;

            

            Rigidbody rb = projectile_01.GetComponent<Rigidbody>();
            rb.velocity = transform.forward * projectileSpeed;

        }
	}
}
