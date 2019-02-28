using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatBehaviour : MonoBehaviour
{
    public enum Behaviours
    {
        Idle,
        wonder,
        chase,
        attack,
        rotate
    }
    public Behaviours behaviours;

    private void Start()
    {
   
    }

    private void Update()
    {
        BehaviourSwitch();
    }

    private void BehaviourSwitch()
    {
        switch (behaviours)
        {
            case Behaviours.Idle:

                Debug.Log("Idle");

                break;

            case Behaviours.wonder:


                break;

            case Behaviours.chase:


                break;

            case Behaviours.attack:


                break;

            case Behaviours.rotate:


                break;
        }
    }
}
