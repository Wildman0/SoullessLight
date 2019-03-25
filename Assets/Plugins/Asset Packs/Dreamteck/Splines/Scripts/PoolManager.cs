using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour {

    public int pooledAmount = 20;
    public GameObject toSpawn;
    public List<GameObject> orbs;

    void Start () {

        orbs = new List<GameObject>();
        for (int i = 0; i < pooledAmount; i++)
        {
            GameObject obj = (GameObject)Instantiate(toSpawn);
            obj.SetActive(false);
            orbs.Add(obj);
        }
    }
}
	
