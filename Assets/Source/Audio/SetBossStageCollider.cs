using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetBossStageCollider : MonoBehaviour 
{
	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			MusicController.instance.SetBossStageParameter(1);
		}
	}
}
