using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dreamteck.Splines;

public class BossProjectile : MonoBehaviour
{
    public SplineFollower follower;
    [SerializeField] private float projectileDamage = 0.1f;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth.instance.TakeDamage(projectileDamage);
            Destroy(gameObject);
        }
    }
}
