using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraEmptyMovement : MonoBehaviour
{
    private GameObject bossGameObject;

    void Awake()
    {
        bossGameObject = GameObject.FindGameObjectWithTag("Boss");
    }

	void FixedUpdate ()
	{
		transform.LookAt((bossGameObject.transform.position));
	}
}
