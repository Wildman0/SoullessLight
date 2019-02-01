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

    public void CoolDownTimer()
    {
        timer = phase.currentPhase.coolDownTimer;

        if (phase.activateAttacking == true)
        {
            countUpTimer += 1f * Time.deltaTime;
            if(countUpTimer >= timer)
            {
                phase.selectAttackStyle = true;
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
