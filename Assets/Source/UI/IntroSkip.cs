using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroSkip : MonoBehaviour {

    public GameObject introScreen;
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.Escape))
            introScreen.SetActive(false);

        if (Input.GetKeyDown(KeyCode.JoystickButton7))
            introScreen.SetActive(false);
    }
}
