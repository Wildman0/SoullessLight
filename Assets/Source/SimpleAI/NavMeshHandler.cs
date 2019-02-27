using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshHandler : MonoBehaviour
{
    public NavMeshAgent agent;
	
	void Start ()
    {
        //Getting NavMeshAgent off of the AI which other scripts use tp refer to
        agent = GetComponent<NavMeshAgent>();
        agent.speed = 3.5f;
	}
	

}
