using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
	public PlayerController playerController;
	
	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
		LookAtBoss();
	}

	private void LookAtBoss()
	{
		transform.LookAt(playerController.targetLookPosition);
	}
}
