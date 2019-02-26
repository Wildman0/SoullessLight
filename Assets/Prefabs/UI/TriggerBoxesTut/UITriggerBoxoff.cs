using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITriggerBoxoff : MonoBehaviour {
    public GameObject UiElement;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            UiElement.SetActive(false);
            Destroy(this);
        }
    }

}
