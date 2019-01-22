using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    public BossPhases bossPhases;

    public static string currentPhase;

    public static bool start;
    private bool restart;
    private bool intermission;

    private void Start()
    {
        currentPhase = "Intro";
    }

    private void Update()
    {
        Phases();
    }

    /// <summary>
    /// Checks the phase and assigns the attack values
    /// </summary>
    private void Phases()
    {
        switch (currentPhase)
        {
            case "Intro":

                Intro();

                break;

            case "PhaseOne":

                Attacks.closeAttacks = bossPhases.phaseOneCloseAttacks;
                Attacks.longAttacks = bossPhases.phaseOneLongAttacks;
                Attacks.coolDownTimer = bossPhases.phaseOneCoolDownTimer;

                Conditions.startChecks = true;

                break;

            case "IntermissionOne":

                Intermission();

                break;

            case "PhaseTwo":

                Attacks.closeAttacks = bossPhases.phaseTwoCloseAttacks;
                Attacks.longAttacks = bossPhases.phaseTwoLongAttacks;
                Attacks.coolDownTimer = bossPhases.phaseTwoCoolDownTimer;

                break;

            case "IntermissionTwo":

                Intermission();

                break;

            case "PhaseThree":

                Attacks.closeAttacks = bossPhases.phaseThreeCloseAttacks;
                Attacks.longAttacks = bossPhases.phaseThreeLongAttacks;
                Attacks.coolDownTimer = bossPhases.phaseThreeCoolDownTimer;

                break;

            case "Death":

                Death();

                break;
            default:
                break;
        }
    }
    
    /// <summary>
    /// Activates boss intro animation if bool is true
    /// </summary>
    private void Intro()
    {
        if (start == true)
        {
            BossAnim.anim.SetBool("StartIntro", BossAnim.anim.GetCurrentAnimatorStateInfo(0).IsName("Start"));
            FindObjectOfType<RFX4_CameraShake1>().PlayShake();
            StartCoroutine(WaitToEnterPhaseOne());

            if (!BossAnim.anim.GetCurrentAnimatorStateInfo(0).IsName("Start"))
            {
                start = false;
            }
        }
    }

    /// <summary>
    /// Changes intro phase to phaseOne phase after an amount of seconds
    /// </summary>
    private IEnumerator WaitToEnterPhaseOne()
    {
        yield return new WaitForSeconds(5f);
        currentPhase = "PhaseOne";
    }

    private void Intermission()
    {

    }

    private void Death()
    {

    }
}
