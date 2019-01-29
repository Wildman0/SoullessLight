using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle : MonoBehaviour {

    public GameObject animator;

    // Use this for initialization
    public void ParticleEffect()
    {
        animator = Instantiate(animator, transform.position, Quaternion.identity) as GameObject;
        //animator.gameObject.SetActive(true);
       //Destroy(gameObject);
    }
    
	
	// Update is called once per frame
	void Update () {
		
	}
}
