using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle : MonoBehaviour {

    public GameObject animator;
    public GameObject animators;
    public GameObject holder;

    // Use this for initialization
    public void ParticleEffect()
    {
        animator.SetActive(true);
        animator = Instantiate(animator, holder.transform.position, Quaternion.identity) as GameObject;
      
    }
    //My anims call functions to create events, I'm creating another one of these so I don't have to make another script e.g. "particle 1 hurr durr"
    public void ParticleEffects()
    {
        animators.SetActive(true);
        animators = Instantiate(animators, holder.transform.position, Quaternion.identity) as GameObject;

    }

    // Update is called once per frame
    void Update () {
		
	}
}
