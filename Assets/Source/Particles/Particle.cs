using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle : MonoBehaviour {

    public GameObject animator;
    public GameObject animators;
    public GameObject animators1;
    public GameObject animators2;
    public GameObject animators3;
    public GameObject animators4;
    public GameObject holder;

    // Use this for initialization
    public void ParticleEffect()
    {
        GameObject Clone;
        animator.SetActive(true);
        Clone = Instantiate(animator, holder.transform.position, Quaternion.identity) as GameObject;
        Destroy(Clone, 5);


    }
    //My anims call functions to create events, I'm creating another one of these so I don't have to make another script e.g. "particle 1 hurr durr"
    public void ParticleEffects()
    {
        GameObject Clone1;
        animators.SetActive(true);
        Clone1 = Instantiate(animators, holder.transform.position, Quaternion.identity) as GameObject;
        Destroy(Clone1, 10);

    }

    public void ParticleEffects1()
    {
        GameObject Clone2;
        animators1.SetActive(true);
        Clone2 = Instantiate(animators1, holder.transform.position, Quaternion.identity) as GameObject;
        Destroy(Clone2, 5);

    }

    public void ParticleEffects2()
    {
        GameObject Clone3;
        animators2.SetActive(true);
        Clone3 = Instantiate(animators2, holder.transform.position, Quaternion.identity) as GameObject;
        Destroy(Clone3, 5);


    }

    public void ParticleEffects3()
    {
        GameObject Clone4;
        animators3.SetActive(true);
        Clone4 = Instantiate(animators3, holder.transform.position, Quaternion.identity) as GameObject;
        Destroy(Clone4, 5);

    }

    public void ParticleEffects4()
    {
        GameObject Clone5;
        animators4.SetActive(true);
        Clone5 = Instantiate(animators4, holder.transform.position, Quaternion.identity) as GameObject;
        Destroy(Clone5, 5);
    }
    // Update is called once per frame
    void Update () {
		
	}
}
