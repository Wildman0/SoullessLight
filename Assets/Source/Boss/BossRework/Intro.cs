using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intro : MonoBehaviour
{
    Phase phase;

    public static bool activateBoss;
    public bool cameraShake;

    public float shakeLength;

    private void Start()
    {
        phase = GetComponent<Phase>();
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
