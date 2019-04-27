using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
	private Vector3 lockOnPosition;
    private float gradualRotate;
    private Vector3 Reset;
	
	// Update is called once per frame
	void Update ()
	{
		if (CameraController.instance.isLocked)
			LookAtTarget();
        //if (PlayerController.instance.GetPlayerState(PlayerActions.Rolling))
            //SmoothRolling();
        else
            LookForward();
        gradualRotate += Time.deltaTime;
	}

	private void LookAtTarget()
	{
		transform.LookAt(new Vector3(CameraController.instance.secondaryTarget.transform.position.x,
			transform.position.y,
			CameraController.instance.secondaryTarget.transform.position.z));
        gradualRotate = 0;

      
    }

	private void LookForward()
	{
        Vector3 direction = (Reset);
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, direction.y, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 10f);

        if (gradualRotate >= 1)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
		
	}

    //private void SmoothRolling()
    //{
     //   gradualRotate = -0.5f;
        //Debug.Log(gradualRotate);
    //}
}
