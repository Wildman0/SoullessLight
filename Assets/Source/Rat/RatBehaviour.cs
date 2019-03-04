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
    Chase chase;

    AnimHandler animHandler;

    private void Start()
    {
        idle = GetComponent<Idle>();
        wonder = GetComponent<Wonder>();
        chase = GetComponent<Chase>();

        animHandler = GetComponent<AnimHandler>();
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

                animHandler.IdleAnimation();

                break;

            case Behaviours.wonder:

                wonder.GoToDestination();

                animHandler.WalkAnimation();

                break;

            case Behaviours.chase:

                chase.ChaseTarget();

                animHandler.ChaseAnimation();

                break;

            case Behaviours.attack:

                animHandler.AttackAnimation();

                break;
        }
    }
}
