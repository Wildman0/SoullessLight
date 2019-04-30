using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapeMenu : MonoBehaviour
{

    public GameObject escapeMenu;
    public bool screenActive;
    
    // Update is called once per frame
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        screenActive = true;
    }
    void LateUpdate()
    {
        InputCheck();           
    }


    void InputCheck()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleEscape();
        }

        if (Input.GetKeyDown(KeyCode.JoystickButton7))
        {
            ToggleEscape();
        }

    }

    void ToggleEscape()
    {
        screenActive = !screenActive;

        if (!screenActive)
        {
            //Debug.Log("Open");
            escapeMenu.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;

        }
        else if (screenActive)
        {
            //Debug.Log("Close");
            escapeMenu.SetActive(false);
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

        }
    }

    public void CursorReturn()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        screenActive = !screenActive;
    }

}
