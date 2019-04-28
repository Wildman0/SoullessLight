using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOnDisable : MonoBehaviour {
    public GameObject uiToDisable;



    private void OnDisable()
    {
        uiToDisable.active = false;
    }
}
