using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlay : MonoBehaviour
{


    [SerializeField] private GameObject audioData;
    private bool isPlayed;


    // Use this for initialization
    void Start()
    {
            audioData.SetActive(true);
            isPlayed = true;  
    }

    private void Update()
    {
        Start();
    }
}
 
