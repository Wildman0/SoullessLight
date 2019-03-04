using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle : MonoBehaviour
{
    private NavMeshHandler navHandler;

    private void Start()
    {
        navHandler = GetComponent<NavMeshHandler>();
    }
}
