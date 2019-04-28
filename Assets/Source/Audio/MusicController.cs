using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD.Studio;

public class MusicController : MonoBehaviour
{
	public static MusicController instance;
	private EventInstance bossBattleMusic;
	private ParameterInstance bossBattleStage;

	void Awake()
	{
		if (!instance)
			instance = this;
		else
			//Debug.LogError("More than one instance of MusicController in the scene");
		
		bossBattleMusic = FMODUnity.RuntimeManager.CreateInstance("event:/MainTheme");
		bossBattleMusic.start();
		bossBattleMusic.getParameter("bossBattleStage", out bossBattleStage);
        bossBattleMusic.setParameterValue("bossBattleStage", 0);
		Debug.Log(bossBattleStage);
        
    }
	
	public void SetBossStageParameter(int i)
	{
		bossBattleMusic.setParameterValue("bossBattleStage", i);
        Debug.Log(i);
	}
}
