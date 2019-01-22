﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class BossHealth : MonoBehaviour
{
    [Range(0, 1)]
    public float health = 0;

    bool active ;

    public UI ui;
    public AudioSource audioSource;
    public AudioSource audioSource1;
    public AudioSource audioSource2;

    private void Update()
    {
        PhaseChangeOnHealth();

        Death();
    }

    public void Damage(float i)
    {
        health -= i;
        //TODO: SORRY ASH, YOU'LL NEED TO GET THIS REFERENCE PROPERLY
        FindObjectOfType<RFX4_CameraShake>().PlayShake();
        BossAnim.anim.SetTrigger("IsBossDamaged");
    }

    private void Death()
    {
        if (health <= 0 && active == false)
        {
            BossAnim.anim.SetTrigger("IsDead");
            ui.BossDefeated.GetComponent<Image>().enabled = true;
            ui.BossDefeated.GetComponent<Animator>().SetTrigger("IsDefeated");
            audioSource.Stop();
            audioSource1.Play();
            StartCoroutine(Conclusion());

            active = true;

        }
    }
    //New Audio After Boss Dies (Isaac)
    private IEnumerator Conclusion()
    {
        yield return new WaitForSecondsRealtime(5f);
        audioSource2.Play();
        Debug.Log("Conclusion");
        
    }


    private void PhaseChangeOnHealth()
    {
        if(health <= 0.65f && health >= 0.35)
        {
            Boss.currentPhase = "PhaseTwo";
        }
        else if (health <= 0.35 && health >= 0.01f)
        {
            Boss.currentPhase = "PhaseThree";
        }
    }
}
