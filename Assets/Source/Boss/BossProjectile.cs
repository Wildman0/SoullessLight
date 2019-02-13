using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dreamteck.Splines;

public class BossProjectile : MonoBehaviour
{
    public bool destroyAfterTime = false;
    public float destroyAfterXTime = 20f;
    public GameObject implosion;
    public SplineFollower follower;
    [SerializeField] private float projectileDamage = 0.1f;

    private void Start()
    {
        if (destroyAfterTime == true)
        {
            Object.Destroy(this.gameObject, destroyAfterXTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Implode();
            PlayerHealth.instance.TakeDamage(projectileDamage);
            Destroy(gameObject);
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

    private void OnDestroy()
    {
        Implode();
    }
}
