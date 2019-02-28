using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dreamteck.Splines;

public class TutorialProjectile : MonoBehaviour
{
    public GameObject implosion;
    public GameObject enableMe;
    public GameObject doorBlocker;
    public GameObject spawn;
    public static int health = 3;
    public SplineFollower follower;

    private void Start()
    {
        enableMe.SetActive(false);
        doorBlocker = GameObject.Find("DoorBlocker");
        StartCoroutine(SpawnComplete());
    
    }

    private void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    void Implode()
    {
        GameObject implo = Instantiate(implosion, transform.position, Quaternion.identity);
        implo.GetComponent<ParticleSystem>().Play();
    }

    IEnumerator SpawnComplete()
    {
        yield return new WaitForSeconds(1.5f);
        enableMe.SetActive(true);
        Destroy(spawn);
    }

    private void OnDestroy()
    {
        Destroy(doorBlocker);
        Implode();
    }
}
