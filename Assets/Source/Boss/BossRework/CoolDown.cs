using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoolDown : MonoBehaviour
{
    Phase phase;

    public float timer;
    public float countUpTimer;

    private void Start()
    {
        phase = GetComponent<Phase>();
    }

    private void Update()
    {
        CoolDownTimer();
    }

    private void CoolDownTimer()
    {
        timer = phase.currentPhase.coolDownTimer;

        if (phase.activateAttacking == true)
        {
            countUpTimer += 1f * Time.deltaTime;
            if(countUpTimer >= timer)
            {
                phase.ChooseAttackAnimation();
                phase.activateAttacking = false;
                countUpTimer = 0f;
            }
        }
        else
        {
            countUpTimer = 0f;
        }
    }
}
