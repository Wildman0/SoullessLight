using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conditions : MonoBehaviour
{
    public static bool inRange;
    public static bool startChecks;
    public static bool isRotating;

    private void Update()
    {
        StartConditionChecks();
    }

    /// <summary>
    /// if bool is true check check functions
    /// </summary>
    private void StartConditionChecks()
    {
        if (startChecks == true)
        {
            InRange();
        }
        else
        {
            Attacks.startAttacks = false;
        }
    }

    /// <summary>
    /// Checks the players is in range of the boss
    /// </summary>
    private void InRange()
    {
        if (inRange == true)
        {
            IsRotating();
        }
    }

    /// <summary>
    /// checks to see if boss is not rotating
    /// </summary>
    private void IsRotating()
    {
        if (isRotating == false)
        {
            IsIdle();
        }
    }

    /// <summary>
    /// Checks to see if boss animation is on Idle
    /// </summary>
    public void IsIdle()
    {
        if (BossAnim.anim.GetCurrentAnimatorStateInfo(0).IsName("A_Idle"))
        {
            EndConditionChecks();
        }
        else
        {
            Attacks.startAttacks = false;
            Attacks.isAttacking = false;
        }
    }

    /// <summary>
    /// After all functions are correct bool turns true so boss can start attacking
    /// </summary>
    private void EndConditionChecks()
    {
        Attacks.startAttacks = true;
    }
}
