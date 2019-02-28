using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VersionNumber : MonoBehaviour 
{
	void Start ()
	{
		GetComponent<Text>().text = AnalyticsManager.current.gameStatus.GetVersionNumber();
	}
}
