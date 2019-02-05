using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
	private GameObject boss;
	private Vector3 lockOnPosition;

	void Start()
	{
		boss = GameObject.FindWithTag("Boss");
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
		if (CameraController.instance.isLocked)
			LookAtBoss();
		else
			LookForward();
	}

	private void LookAtBoss()
	{
		transform.LookAt(new Vector3(boss.transform.position.x,
			transform.position.y,
			boss.transform.position.z));
	}

	private void LookForward()
	{
		transform.LookAt(new Vector3(-Camera.main.transform.position.x,
			transform.position.y,
			- Camera.main.transform.position.z));
	}
}
