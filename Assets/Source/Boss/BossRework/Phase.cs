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
    public BossHealth bossHealth;
    public PhaseValues currentPhase;
    public AttackAnimations attackAnimations;
    public List<AnimationClip> currentAttackAnimationClips = new List<AnimationClip>();
    public AnimatorOverrideController animatorOverrideController;
    private Distance distance;
    private CoolDown coolDown;
    public Animator anim;
    private AttackCombo attackCombo;

    [HideInInspector]
    public int animationIndex;
    [HideInInspector]
    public int lastIndex;

    public float comboChance;

    public bool activateAttacking;
    public bool intermission;
    public bool selectAttackStyle;
    public static bool isAttacking;

    private void Start()
    {
        anim = GetComponent<Animator>();
        anim.runtimeAnimatorController = animatorOverrideController;
        ////animatorOverrideController = new AnimatorOverrideController(anim.runtimeAnimatorController);
        distance = GetComponent<Distance>();
        coolDown = GetComponent<CoolDown>();
        attackCombo = GetComponent<AttackCombo>();
    }

    private void Update()
    {
        Intermission();
    }

    private void Intermission()
    {
        if(intermission == false)
        {
            SelectPhase();
        }
        else
        {
            currentPhase = null;
        }
    }

    private void SelectPhase()
    {
        Profiler.BeginSample("Phase Script");
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
        Profiler.EndSample();
    }

    private void GetAttackAnimations()
    {
        attackAnimations = distance.showDesignersDistance == "Close"? attackAnimations = currentPhase.closeAttacks :
                           distance.showDesignersDistance == "Mid"?   attackAnimations = currentPhase.midAttacks :
                           distance.showDesignersDistance == "Long"?  attackAnimations = currentPhase.longAttacks :
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
        if (selectAttackStyle == false)
        {
            comboChance = Random.Range(0.0f, 1.0f);
            if (comboChance > currentPhase.comboChance)
            {
                attackCombo.SelectComboAnimations();
                selectAttackStyle = true;
            }
            else
            {
                ChooseAttackAnimation();
                selectAttackStyle = true;
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

            StartCoroutine(Wait());
        }
    }

    public IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.6f);
        anim.SetBool("Attack", false);
        lastIndex = animationIndex;
        isAttacking = false;
        selectAttackStyle = false;
    }

    private void SelectNewIndex()
    {
        animationIndex = Random.Range(0, currentAttackAnimationClips.Count);
        ChooseAttackAnimation();
    }

    private IEnumerator test()
    {
        yield return new WaitForSeconds(0.3f);
        anim.SetBool("Combo", false);
    }
}
