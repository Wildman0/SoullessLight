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
	void Update ()
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
		transform.rotation = Quaternion.Euler(0, 0, 0);
	}
}
