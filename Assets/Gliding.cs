using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gliding : MonoBehaviour {

    public Animator IntroGlider;

	// Use this for initialization
	void OnTriggerEnter (Collider other)
    {
        IntroGlider.SetTrigger("Glide");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
