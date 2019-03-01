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

    Idle idle;
    Wonder wonder;

    private void Start()
    {
        idle = GetComponent<Idle>();
        wonder = GetComponent<Wonder>();
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

                idle.Pause();

                break;

            case Behaviours.wonder:

                wonder.GoToDestination();

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
