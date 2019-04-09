using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    private bool doorIsOpen;
    [SerializeField] private Animator doorAnimator;
    [SerializeField] private GameObject DoorBlocker;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            doorAnimator.SetTrigger("isOpen");
            StartCoroutine(Door());
            
        }
    }

    private IEnumerator Door()
    {
        yield return new WaitForSeconds(3);
        DoorBlocker.SetActive(false);
    }
}
