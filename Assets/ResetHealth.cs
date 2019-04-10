using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetHealth : MonoBehaviour {

    PlayerHealth pH;



    private void Start()
    {
        pH = PlayerHealth.instance;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {

            pH.healCount = 3;
            pH.health = 1f;
        }
    }
}
