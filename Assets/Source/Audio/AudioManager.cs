using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip backGroundAudio;
    private AudioSource audioSource;

    public static bool canPlay;

	void Start ()
	{
	    audioSource = GetComponent<AudioSource>();
	    audioSource.clip = backGroundAudio;
	}
	
	void Update ()
	{
		PlayBackGroundAudio();
	}

    private void PlayBackGroundAudio()
    {
        if (canPlay == false)
        {
            StartCoroutine(WaitToPlay());
        }
    }

    private IEnumerator WaitToPlay()
    {
        yield return new WaitForSeconds(4f);
        audioSource.Play();
        canPlay = true;
    }
}
