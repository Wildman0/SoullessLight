using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wonder : MonoBehaviour
{
    PathManager pathManager;
    NavMeshHandler navHandler;

    Rigidbody rig;
    public GameObject[] aiPaths;
    public GameObject currentPath;
    public int index;
    public int lastRandomNumber;
	
	void Start ()
    {
        pathManager = GetComponent<PathManager>();

        navHandler = GetComponent<NavMeshHandler>();

        rig = GetComponent<Rigidbody>();
        aiPaths = GameObject.FindGameObjectsWithTag("Path");

        //Int picks a random number between 0 and the array length
        index = Random.Range(0, aiPaths.Length);
        currentPath = aiPaths[index];

        rig.useGravity = false;
        lastRandomNumber = index;
    }

	void Update ()
    {
        //Checks to see if isWond is true, if so the AI will go to the current path position
        if (pathManager.isWond == true)
        {
            navHandler.agent.destination = currentPath.transform.position;
        }

        IsAIWalking();
	}

    //Function checks to see if index dose not pick the same number
    void IsAIWalking()
    {
        if (index == lastRandomNumber)
        {
            index = Random.Range(0, aiPaths.Length);
            currentPath = aiPaths[index];
        }
    }

    //Changes number when hitting a path
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Path")
        {
            lastRandomNumber = index;
            RandomDestination();
        }
    }

    //picks another number e.g new path to go to
    void RandomDestination()
    {
        if (index == lastRandomNumber)
        {
            index = Random.Range(0, aiPaths.Length);
            currentPath = aiPaths[index];
        }
    }
}
