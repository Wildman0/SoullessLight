using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Brightness : MonoBehaviour {

    public Button buttonSelect;
    // Use this for initialization
    void Start () {
		
	}

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.JoystickButton1))
            buttonSelect.Select();

    }
}
