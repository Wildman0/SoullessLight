using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraEmptyMovement : MonoBehaviour
{
	void FixedUpdate ()
	{
		if (CameraController.instance.isLocked)
			transform.LookAt((CameraController.instance.secondaryTarget.transform.position));
	}
}
