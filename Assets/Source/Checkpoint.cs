using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour {

    private Spawning sp;

    void Start()
    {
        sp = GameObject.FindGameObjectWithTag("SP").GetComponent<Spawning>();
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            sp.lastCheckpointPos = transform.position;
        }
    }
}
