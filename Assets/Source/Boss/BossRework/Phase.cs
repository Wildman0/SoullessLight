using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.Profiling;

public class Phase : MonoBehaviour
{
    public List<PhaseValues> phaseValues = new List<PhaseValues>();
    private BossHealth bossHealth;
    public PhaseValues currentPhase;
    public AttackAnimations attackAnimations;
    public List<AnimationClip> currentAttackAnimationClips = new List<AnimationClip>();
    public AnimatorOverrideController animatorOverrideController;
    private Distance distance;
    private CoolDown coolDown;
    public Animator anim;
    private AttackCombo attackCombo;
    private Intermission intermission;

   
    public int animationIndex;
    public int lastIndex;

    public float comboChance;
    private float timer;

    public bool activateAttacking;
    public bool intermissionCheck;
    public bool selectAttackStyle;
    public static bool isAttacking;
    public bool retrievedPhase;

    public string phaseSwitchingCheck;

    private void Start()
    {
        anim = GetComponent<Animator>();
        anim.runtimeAnimatorController = animatorOverrideController;
        ////animatorOverrideController = new AnimatorOverrideController(anim.runtimeAnimatorController);
        distance = GetComponent<Distance>();
        coolDown = GetComponent<CoolDown>();
        attackCombo = GetComponent<AttackCombo>();
        intermission = GetComponent<Intermission>();
        bossHealth = GetComponent<BossHealth>();

        timer = 0.4f;
    }

    private void Update()
    {
        HasPhaseChanged();
    }


    private void HasPhaseChanged()
    {
        timer -= 1f * Time.deltaTime;
        if (timer <= 0)
        {
            if (currentPhase != null)
            {
                if (retrievedPhase == false)
                {
                    phaseSwitchingCheck = currentPhase.phaseName;
                    retrievedPhase = true;
                    intermission.activated = false;
                    intermission.listNumber += 1;

                    timer = .4f;

                    anim.SetBool("IntermissionOut", false);
                }
                else if (phaseSwitchingCheck != currentPhase.phaseName)
                {
                    intermissionCheck = true;
                    timer = .4f;
                }
                else
                {
                    timer = .4f;
                }
            }
            else
            {
                timer = .4f;
            }
        }

        Intermission();
    }

    private void Intermission()
    {
        if(intermissionCheck == false)
        {
            SelectPhase();
            AttackManagement();
        }
        if(intermissionCheck == true)
        {
            intermission.PlayerAnimation();
        }
    }

    private void SelectPhase()
    {
        if(anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            currentPhase = phaseValues.Find(x => x.min >= bossHealth.health && x.max <= bossHealth.health);
            GetAttackAnimations();
            coolDown.CoolDownTimer();

            activateAttacking = true;
        }
        else
        {
            activateAttacking = false;
        }
    }

    private void GetAttackAnimations()
    {
        attackAnimations = distance.showDesignersDistance == "Close"? attackAnimations = currentPhase.closeAttacks:
                           distance.showDesignersDistance == "Mid"? attackAnimations = currentPhase.midAttacks :
                           distance.showDesignersDistance == "Long"? attackAnimations = currentPhase.longAttacks :
                                                                      attackAnimations = null;

        if (PlayerDirection.direction == "Left")
        {
            currentAttackAnimationClips = attackAnimations.leftAttacks;
        }
        else if (PlayerDirection.direction == "Right")
        {
            currentAttackAnimationClips = attackAnimations.rightAttacks;
        }
    }

    public void AttackManagement()
    {
        if (selectAttackStyle == true)
        {
            comboChance = Random.Range(0.0f, 1.0f);
            if (comboChance > currentPhase.comboChance)
            {
                attackCombo.selectAttack = true;
                selectAttackStyle = false;
            }
            else
            {
                ChooseAttackAnimation();;
            }
        }
    }

    public void ChooseAttackAnimation()
    {
        if (lastIndex == animationIndex)
        {
            SelectNewIndex();
        }
        else
        {
            animatorOverrideController["ATTACK"] = currentAttackAnimationClips[animationIndex];
            isAttacking = true;
            anim.SetBool("Attack", true);
            selectAttackStyle = false;

            StartCoroutine(Wait());
        }
    }

    public IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.6f);
        anim.SetBool("Attack", false);
        lastIndex = animationIndex;
        isAttacking = false;
    }

    public void SelectNewIndex()
    {
        animationIndex = Random.Range(0, currentAttackAnimationClips.Count);
    }
}
