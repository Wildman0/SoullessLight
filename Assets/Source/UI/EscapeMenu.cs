using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapeMenu : MonoBehaviour
{

    public GameObject escapeMenu;

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
            escapeMenu.SetActive(true);

        else if (Input.GetKeyDown(KeyCode.Escape))
            escapeMenu.SetActive(false);

    }
}
