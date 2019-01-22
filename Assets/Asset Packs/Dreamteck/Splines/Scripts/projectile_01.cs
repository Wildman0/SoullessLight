using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dreamteck.Splines;


public class projectile_01 : MonoBehaviour{

 

    public SplineFollower follower;
  

    // destroys the projectile if colliding with something else (this is where the different types of projectiles will differ.)
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "projectile"){
            return;
        }
        if (other.tag == "Boss")
        {
            return;
        }
        else
        {
            Destroy(gameObject);
        }
     
    }


 
}
