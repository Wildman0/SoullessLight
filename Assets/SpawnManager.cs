using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour {

    public Transform spawnLocation;
    public Transform Caves;
    public Transform Boss;


    void OnColliderEnter (Collider col)
    {
        if (col.tag == "Caves")
        {
            spawnLocation = Caves;
            Debug.Log(Caves);
        }
        if (col.tag == "Arena")
        {
            spawnLocation = Boss;
        }
    }
}
