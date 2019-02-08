using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dreamteck.Splines;

public class BossProjectile : MonoBehaviour
{
    public SplineFollower follower;
  
    // destroys the projectile if colliding with something else (this is where the different types of projectiles will differ.)
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
