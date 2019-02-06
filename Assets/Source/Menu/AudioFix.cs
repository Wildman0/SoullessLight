using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioFix : MonoBehaviour
{

    public float delay = 1f;
    void Start()
    {
        if (gameObject.activeInHierarchy)
            gameObject.SetActive(true);

        StartCoroutine(Deactivate());
    }

    IEnumerator Deactivate()
    {

        yield return new WaitForSeconds(delay);

        gameObject.SetActive(false);

        StopCoroutine(Deactivate());

    }

}


