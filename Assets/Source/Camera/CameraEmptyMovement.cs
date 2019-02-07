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
		if (CameraController.instance.isLocked)
			transform.LookAt((bossGameObject.transform.position));
	}
}
