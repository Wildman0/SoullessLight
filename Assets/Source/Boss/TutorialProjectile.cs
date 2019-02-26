using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dreamteck.Splines;

public class TutorialProjectile : MonoBehaviour
{
    public GameObject implosion;
    public GameObject enableMe;
    public GameObject spawn;
    private int health = 3;
    public SplineFollower follower;

    private void Start()
    {
        enableMe.SetActive(false);
        
        StartCoroutine(SpawnComplete());
    
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            health = health - 1;

            if (health == 0)
            {
                Destroy(this);
            }
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
        //Debug.Log("It's working");
        Destroy(spawn);
    }

    private void OnDestroy()
    {
        Implode();
    }
}
