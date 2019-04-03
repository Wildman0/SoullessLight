using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtBoxTutorial : MonoBehaviour {

    public GameObject me;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
                PlayerHealth.instance.TakeDamage(0.3f);
                gameObject.active = false;
        }
    }


}
