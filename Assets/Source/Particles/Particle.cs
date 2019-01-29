using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle : MonoBehaviour {

    public GameObject animator;
    public GameObject holder;

    // Use this for initialization
    public void ParticleEffect()
    {
        animator.SetActive(true);
        animator = Instantiate(animator, holder.transform.position, Quaternion.identity) as GameObject;
      
    }
    
	
	// Update is called once per frame
	void Update () {
		
	}
}
