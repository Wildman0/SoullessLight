using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Intro : MonoBehaviour
{
    Phase phase;

    public static bool activateBoss;
    public bool cameraShake;
    public GameObject bossUI;

    public float shakeLength;

    private void Start()
    {
        phase = GetComponent<Phase>();
        bossUI.SetActive(false);
    }

    private void Update()
    {
        ActivateBoss();
        StartCoroutine(CameraShakeLength(shakeLength));
    }

    private void ActivateBoss()
    {
        if(activateBoss == true && phase.anim == phase.anim.GetCurrentAnimatorStateInfo(0).IsName("Start"))
        {
            phase.anim.SetBool("Start", phase.anim.GetCurrentAnimatorStateInfo(0).IsName("Start"));
            cameraShake = true;
            bossUI.SetActive(true);
            MusicController.instance.SetBossStageParameter(1);
        }
        else if (phase.anim != phase.anim.GetCurrentAnimatorStateInfo(0).IsName("Start"))
        {
            activateBoss = false;
        }
    }

    private IEnumerator CameraShakeLength(float time)
    {
        if(cameraShake == true)
        {
            FindObjectOfType<RFX4_CameraShake1>().PlayShake();

            yield return new WaitForSeconds(time);
            cameraShake = false;
        }
    }

}
