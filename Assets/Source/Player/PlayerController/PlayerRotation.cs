using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
	public PlayerController playerController;
	private GameObject boss;
	private Vector3 lockOnPosition;

	void Start()
	{
		boss = GameObject.FindWithTag("Boss");

	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
		LookAtBoss();
	}

	private void LookAtBoss()
	{
		transform.LookAt(new Vector3(boss.transform.position.x,
			transform.position.y,
			boss.transform.position.z));
	}
}
