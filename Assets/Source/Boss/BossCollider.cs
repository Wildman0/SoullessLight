using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCollider : MonoBehaviour
{
    public PlayerController playerController;
    private HitReg hitReg;

    public int calledAmount;
    private float damage = 0.1f;
    public static bool isRotating;

    private void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        hitReg = gameObject.GetComponent<HitReg>();
    }

    private void Update()
    {
        HitDetection();
    }

    private void HitDetection()
    {
        if (Phase.isAttacking == true)
        {
            calledAmount += 1;
            if (calledAmount == 1)
            {
                hitReg.ToggleHitreg();
            }
        }
        else
        {
            calledAmount = 0;
        }
    }   
}
