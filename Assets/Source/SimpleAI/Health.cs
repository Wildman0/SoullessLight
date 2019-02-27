using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    HealthManager healthManager;
    public float health;

    private void Start()
    {
        //Finds and gets script from gameobject tag Health manager
        healthManager = GameObject.FindGameObjectWithTag("HealthManager").GetComponent<HealthManager>();

        //sets health to the value of the healthoverride float
        health = healthManager.healthOverride;
    }

    void Update()
    {
        //If health hits 0 then the AI will be destroyed
        if (health < 0)
        {
            Destroy(gameObject);
        }
    }
}
