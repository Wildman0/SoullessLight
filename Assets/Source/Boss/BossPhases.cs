using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Source/Boss/BossPhases", menuName = "BossValues", order = -1)]
public class BossPhases : ScriptableObject
{
    public enum Phases
    {
        Intro,
        PhaseOne,
        PhaseTwoIntro,
        PhaseTwo,
        PhaseThreeIntro,
        PhaseThree
    };
    public static Phases phase;

    public string[] phaseOneCloseAttacks;
    public string[] phaseOneLongAttacks;
    public float phaseOneCoolDownTimer;

    public string[] phaseTwoCloseAttacks;
    public string[] phaseTwoLongAttacks;
    public float phaseTwoCoolDownTimer;

    public string[] phaseThreeCloseAttacks;
    public string[] phaseThreeLongAttacks;
    public float phaseThreeCoolDownTimer;

    public float phaseTwoIntroIntermission;
    public float phaseThreeIntroIntermission;

    public void Start()
    {
        //Debug.Log("OK");
    }
}
