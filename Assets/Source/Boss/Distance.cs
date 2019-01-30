using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting;
using UnityEngine;

public class Distance : MonoBehaviour
{
    private GameObject playerObj;

    public float distance;

    public float closeMinDistance;
    public float closeMaxDistance;
    public float midMinDistance;
    public float midMaxDistance;
    public float longMinDistance;
    public float longMaxDistance;

    public static string showDistance;
    public string showDesignersDistance; // This is for designers so they can see the distance of the player from the boss

    public AudioSource audioSource;
    public AudioClip audioClip;
    public AudioSource audioSource1;
    private bool isOn;

    void Start ()
    {
        playerObj = GameObject.FindGameObjectWithTag("Player");
        audioSource.clip = audioClip;
        CalculateDistanceFromBoss();
        SetDistance(closeMinDistance, closeMaxDistance, longMinDistance, longMaxDistance);
    }
	
	void Update ()
    {
        CalculateDistanceFromBoss();
        SetDistance(closeMinDistance, closeMaxDistance, longMinDistance, longMaxDistance);

        ActivateBoss();
    }

    //Calculates the distance between the boss and the player
    public void CalculateDistanceFromBoss()
    {
        distance = Vector3.Distance(transform.position, playerObj.transform.position);
    }

    public void SetDistance(float closeMinDistance, float closeMaxDistance, float longMinDistance, float longMaxDistance)
    {
        showDistance = distance <= closeMaxDistance && distance >= closeMinDistance ?  "Close" :
        showDistance = distance <= midMaxDistance && distance >= midMinDistance ? "Mid" :
        showDistance = distance <= longMaxDistance && distance >= longMinDistance ? "Long" : "Not in range";

        showDesignersDistance = showDistance; // this is for the designers to see in the inspector
    }

    private void ActivateBoss()
    {
        if (distance <= 6.5f)
        {
            //Boss.start = true;
            Intro.activateBoss = true;
            //Changing Audio for Trigger
            if (isOn == false)
            {
                audioSource.Play();
                audioSource1.Stop();
                isOn = true;
            }
        }

    }
    
}
  