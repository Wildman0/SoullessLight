using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stopper : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "projectile")
        {
            Destroy(other);
        }
        else
        {
            return;
        }
    }
}
