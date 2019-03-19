using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
	private Vector3 lockOnPosition;
	
	// Update is called once per frame
	void Update ()
	{
		if (CameraController.instance.isLocked)
			LookAtTarget();
		else
			LookForward();
	}

	private void LookAtTarget()
	{
		transform.LookAt(new Vector3(CameraController.instance.secondaryTarget.transform.position.x,
			transform.position.y,
			CameraController.instance.secondaryTarget.transform.position.z));
	}

	private void LookForward()
	{
		transform.rotation = Quaternion.Euler(0, 0, 0);
	}
}
