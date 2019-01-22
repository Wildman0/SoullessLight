using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIAppear : MonoBehaviour
{
    [SerializeField] private GameObject customImage;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            customImage.SetActive(true);
            customImage.GetComponent<Animator>().SetTrigger("Caution");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            customImage.GetComponent<Animator>().SetTrigger("NoCaution");
        }
    }
}
