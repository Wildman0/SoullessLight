using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dreamteck.Splines;

public class SpawnerIntermission : MonoBehaviour {

    public float fireTime = 1;
    public float fireDelay = 1;
    public GameObject toSpawn;
    public int pooledAmount = 20;
    List<GameObject> orbs;
    public float speed = 5f;


    private void Start()
    {
        SplineFollower follower = toSpawn.GetComponent<SplineFollower>();
        follower.followSpeed = speed;

        


        orbs = new List<GameObject>();
        for (int i = 0; i < pooledAmount; i++)
        {
            GameObject obj = (GameObject)Instantiate(toSpawn);
            obj.SetActive(false);
            orbs.Add(obj);
        }

        InvokeRepeating("Fire", fireTime, fireDelay);

    }

    void Fire()
    {
        for (int i = 0; i < orbs.Count; i++)
        {
            if (!orbs[i].activeInHierarchy)
            {
                orbs[i].transform.position = transform.position;
                orbs[i].transform.rotation = transform.rotation;
                orbs[i].SetActive(true);
                break;
            }
        }
    }

}
