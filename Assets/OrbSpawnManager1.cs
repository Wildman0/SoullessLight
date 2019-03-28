using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbSpawnManager1 : MonoBehaviour {

    public SpawnerIntermission SIi;

    public void EnableVelocitySpawner1()
    {
        SIi.enabled = true;
            
    }

    public void DisableVelocitySpawner1()
    {
        SIi.enabled = false;
        SIi.spawnAmount = 0;

    }



}
