using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerIntermission : MonoBehaviour {

    public string objectName;

    ObjectPooler objectPooler;

    private void Start()
    {
        objectPooler = ObjectPooler.instance;
    }

    private void FixedUpdate()
    {
        objectPooler.SpawnFromPool("Orb", transform.position, Quaternion.identity);
    }

}
