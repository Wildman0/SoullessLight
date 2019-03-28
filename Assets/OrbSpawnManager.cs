using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbSpawnManager : MonoBehaviour {

    public SpawnerIntermission SI;

    public void EnableVelocitySpawner()
    {
        SI.enabled = true;
            
    }

    public void DisableVelocitySpawner()
    {
        SI.enabled = false;

    }



}
