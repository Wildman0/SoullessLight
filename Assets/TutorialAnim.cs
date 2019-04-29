using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialAnim : MonoBehaviour {

     public static Animator tutorialOrb;
     public GameObject TutOrb;

    private void Start()
    {
        tutorialOrb = TutOrb.GetComponent<Animator>();
    }

}
