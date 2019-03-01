using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dreamteck.Splines;

public class BossProjectile : MonoBehaviour
{
    public bool destroyAfterTime = false;
    public float destroyAfterXTime = 20f;
    public GameObject implosion;
    public GameObject enableMe;
    public GameObject spawn;
    public SplineFollower follower;
    [SerializeField] private float projectileDamage = 0.1f;

    private void Start()
    {
        enableMe.SetActive(false);
        
        StartCoroutine(SpawnComplete());

        if (destroyAfterTime == true)
        {
            Object.Destroy(this.gameObject, destroyAfterXTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (!PlayerHealth.instance.isInvincible)
            {
                Implode();
                PlayerHealth.instance.TakeDamage(projectileDamage);
                Destroy(gameObject);
            }
        }

        if (other.CompareTag("ProjectileStopper"))
        {
            Implode();
            Destroy(gameObject);
            Debug.Log("Boom");
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
