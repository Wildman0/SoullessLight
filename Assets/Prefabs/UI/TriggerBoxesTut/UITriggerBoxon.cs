using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITriggerBoxon : MonoBehaviour {
    public GameObject UiElement;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            UiElement.SetActive(true);
            Destroy(this);
        }
    }

}
