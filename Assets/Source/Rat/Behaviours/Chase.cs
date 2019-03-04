using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase : MonoBehaviour
{
    private RatBehaviour ratBehaviour;
    private NavMeshHandler navHandler;

    private GameObject target;

    public float distance;

    private void Start()
    {
        ratBehaviour = GetComponent<RatBehaviour>();
        navHandler = GetComponent<NavMeshHandler>();

        target = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        CalculateDistanceFromPlayer();
        ChaseTrigger();
    }

    private void CalculateDistanceFromPlayer()
    {
        distance = Vector3.Distance(transform.position, target.transform.position);
    }

    private void ChaseTrigger()
    {
        if(distance <= 6f)
        {
            ratBehaviour.behaviours = RatBehaviour.Behaviours.chase;
        }
        else if(distance >= 6f)
        {
            ratBehaviour.behaviours = RatBehaviour.Behaviours.wonder;
        }

        if(ratBehaviour.behaviours == RatBehaviour.Behaviours.chase && distance <= 3f)
        {
            ratBehaviour.behaviours = RatBehaviour.Behaviours.attack;
        }
    }

    public void ChaseTarget()
    {
        navHandler.agent.destination = target.transform.position;
    }

}
