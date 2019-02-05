using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LowHealthVibration : MonoBehaviour
{
	public static LowHealthVibration instance;

	private bool isEnabled;

	private float lastBeatTime;
	private float timeBetweenBeats = 1.0f;
	
	private void Awake()
	{
		if (!instance)
			instance = this;
		else
			Debug.LogError("More than one instance of LowHealth Vibration in the scene");
	}

	public void SetVibration(bool b)
	{
		isEnabled = b;
	}
	
	void Update()
	{
		if (isEnabled)
		{
			if (lastBeatTime + timeBetweenBeats < Time.time)
			{
				StartCoroutine(VibrationIEnum());
				lastBeatTime = Time.time;
				Debug.Log("beat");
			}
		}
	}

	private IEnumerator VibrationIEnum()
	{
		Controller.Vibrate(0, 0.35f, 0.1f);
		yield return new WaitForSeconds(0.27f);
		Controller.Vibrate(0, 0.35f, 0.1f);
	}
}
