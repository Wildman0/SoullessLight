using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dreamteck.Splines;

public class BossProjectile : MonoBehaviour
{

    public GameObject implosion;
    public SplineFollower follower;
    [SerializeField] private float projectileDamage = 0.1f;

    private void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Implode();
            PlayerHealth.instance.TakeDamage(projectileDamage);
            Destroy(gameObject);
        }
    }


    void Implode()
    {
        GameObject implo = Instantiate(implosion, transform.position, Quaternion.identity);
        implo.GetComponent<ParticleSystem>().Play();
    }
}
