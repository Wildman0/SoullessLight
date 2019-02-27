using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RatMovement : MonoBehaviour
{
    public Transform pathobject;
    public NavMeshAgent nav;

    private void Update()
    {
        FollowPath();
    }

    private void FollowPath()
    {
        nav.SetDestination(pathobject.transform.position);
    }
}
