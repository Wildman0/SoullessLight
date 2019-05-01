using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroSkip : MonoBehaviour {

    public GameObject introScreen;
    public AudioSource mainTheme;
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            mainTheme.Play();
            introScreen.SetActive(false);
        }
           
            

        if (Input.GetKeyDown(KeyCode.JoystickButton7))
        {
            mainTheme.Play();
            introScreen.SetActive(false);
        }
           
            
    }
}
