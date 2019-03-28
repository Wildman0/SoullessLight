using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD.Studio;

public class MusicController : MonoBehaviour
{
	private EventInstance bossBattleMusic;
	private ParameterInstance bossBattleStage;

	void Awake()
	{
		bossBattleMusic = FMODUnity.RuntimeManager.CreateInstance("event:/MainTheme");
		bossBattleMusic.start();
		bossBattleMusic.getParameter("bossBattleStage", out bossBattleStage);
		Debug.Log(bossBattleStage);
	}
	
	public void SetBossStageParameter(int i)
	{
		bossBattleMusic.setParameterValue("bossBattleStage", i);
	}
}
