using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoolDown : MonoBehaviour
{
    Phase phase;

    private float timer;

    private void Start()
    {
        phase = GetComponent<Phase>();
    }

    private void Update()
    {
        
    }

    private void CoolDownTimer()
    {
        if(phase.activateAttacking == true)
        {
            
        }
    }

}
