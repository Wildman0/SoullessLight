using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wonder : MonoBehaviour
{
    private NavMeshHandler navHandler;
    private AnimHandler animHandler;

    private GameObject[] paths;
    private GameObject currentPath;

    private int index;
    private int lastIndex;

    private void Start()
    {
        navHandler = GetComponent<NavMeshHandler>();
        animHandler = GetComponent<AnimHandler>();

        paths = GameObject.FindGameObjectsWithTag("Path");

        index = Random.Range(0, paths.Length);
        currentPath = paths[index];
    }

    public void GoToDestination()
    {
        animHandler.anim.SetBool("isWalking", true);

        navHandler.agent.destination = currentPath.transform.position;

        RandomDestination();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Path")
        {
            lastIndex = index;
            RandomDestination();
        }
    }

    void RandomDestination()
    {
        if (index == lastIndex)
        {
            index = Random.Range(0, paths.Length);
            currentPath = paths[index];
        }
    }
}
