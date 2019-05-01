using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinematicPusher : MonoBehaviour
{
	public bool isMoving = false;
	private static CinematicPusher c;
	
	void Awake()
	{
		c = this;
	}
	
	void Update ()
	{
		if (isMoving)
			transform.position = transform.position + Vector3.forward * Time.deltaTime * 4;
	}

	public static void Trigger()
	{
		c.StartPush();	
	}
	
	void StartPush()
	{
		transform.position = new Vector3(-63.0f, 47.2f, -26.2f);
		isMoving = true;
        StartCoroutine(DestroyAfterTime());
	}

	IEnumerator DestroyAfterTime()
	{
		yield return new WaitForSeconds(5.0f);
		GameObject.Destroy(gameObject);
	}
}
