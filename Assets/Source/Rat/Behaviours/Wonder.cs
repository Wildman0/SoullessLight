using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wonder : MonoBehaviour
{
    private NavMeshHandler navHandler;

    private GameObject[] paths;
    public GameObject currentPath;

    public int index;
    public int lastIndex;

    public float distance;

    private void Start()
    {
        navHandler = GetComponent<NavMeshHandler>();

        paths = GameObject.FindGameObjectsWithTag("Path");

        index = Random.Range(0, paths.Length);
        currentPath = paths[index];
    }

    private void Update()
    {
        ChangeDestination();
    }

    public void GoToDestination()
    {
        navHandler.agent.destination = currentPath.transform.position;

        RandomDestination();
    }

    private void ChangeDestination()
    {
        distance = Vector3.Distance(transform.position, paths[index].transform.position);
        if(distance <= 0.42f)
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
