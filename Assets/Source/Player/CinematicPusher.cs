using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinematicPusher : MonoBehaviour
{
	public bool isMoving = false;
	
	void Update ()
	{
		if (isMoving)
			transform.position = transform.position + Vector3.forward * Time.deltaTime * 4;
	}
}
