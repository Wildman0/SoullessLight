using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasePlayer : MonoBehaviour
{
    PathManager pathManager;
    NavMeshHandler navHandler;

    public GameObject player;

	void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        navHandler = GetComponent<NavMeshHandler>();
        pathManager = GetComponent<PathManager>();
	}
	
	
	void Update ()
    {
        //Check Pathmanager script to see if isChasing is true
        if (pathManager.isChasing == true)
        {
            //Gets NavmeshAgent from navmaeshandler and makes it follow the player
            navHandler.agent.destination = player.transform.position;
        }
	}
}
