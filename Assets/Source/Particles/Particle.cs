using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle : MonoBehaviour {

    public GameObject animator;
    public GameObject animators;
    public GameObject animators1;
    public GameObject animators2;
    public GameObject animators3;
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

    public void ParticleEffects1()
    {
        animators1.SetActive(true);
        animators1 = Instantiate(animators1, holder.transform.position, Quaternion.identity) as GameObject;

    }

    public void ParticleEffects2()
    {
        animators2.SetActive(true);
        animators2 = Instantiate(animators2, holder.transform.position, Quaternion.identity) as GameObject;

    }

    public void ParticleEffects3()
    {
        animators3.SetActive(true);
        animators3 = Instantiate(animators3, holder.transform.position, Quaternion.identity) as GameObject;

    }

    // Update is called once per frame
    void Update () {
		
	}
}
