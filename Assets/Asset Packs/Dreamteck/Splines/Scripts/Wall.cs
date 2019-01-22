using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour {


    public float health;
	
	// Checks Health if Wall is dead at <0

	void Update () {
        Debug.Log(health);

        if (health <= 0)
        {

            Destroy(gameObject);

        }

	}
}
