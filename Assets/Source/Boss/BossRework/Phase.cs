using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class Phase : MonoBehaviour
{
    public List<PhaseValues> phaseValues = new List<PhaseValues>();
    public BossHealth bossHealth;
    public PhaseValues currentPhase;
    private AttackAnimations attackAnimations;
    public List<AnimationClip> currentAttackAnimationClips = new List<AnimationClip>();
    public AnimatorOverrideController animatorOverrideController;
    private Distance distance;
    public Animator anim;

    public float timer;
    public int animationIndex;

    public bool activateAttacking;

    private void Start()
    {
        anim = GetComponent<Animator>();
        anim.runtimeAnimatorController = animatorOverrideController;
        ////animatorOverrideController = new AnimatorOverrideController(anim.runtimeAnimatorController);
        distance = GetComponent<Distance>();

        SelectPhase();
    }

    private void Update()
    {
        SelectPhase();
        BeginAttacking();
    }

    private void SelectPhase()
    {
        if(anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            currentPhase = phaseValues.Find(x => x.min <= bossHealth.health && x.max >= bossHealth.health);
            Debug.Log(currentPhase.phaseName);

            timer = currentPhase.coolDownTimer;

            activateAttacking = true;
        }
    }

    private void BeginAttacking()
    {
        if(activateAttacking == true)
        {
            GetAttackAnimations();
            AttackCoolDown();
        }
    }

    private void GetAttackAnimations()
    {
        if (distance.showDesignersDistance == "Close")
        {
            attackAnimations = currentPhase.closeAttacks;

            Debug.Log("close range" + attackAnimations);
        }
        else if (distance.showDesignersDistance == "Mid")
        {
            attackAnimations = currentPhase.midAttacks;

            Debug.Log("mid range" + attackAnimations);
        }
        else if (distance.showDesignersDistance == "Long")
        {
            attackAnimations = currentPhase.longAttacks;

            Debug.Log("long range" + attackAnimations);
        }

        if (PlayerDirection.direction == "Left")
        {
            Debug.Log("Left");

            currentAttackAnimationClips = attackAnimations.leftAttacks;

            Debug.Log("left direction" + currentAttackAnimationClips);
        }
        else if (PlayerDirection.direction == "Right")
        {
            Debug.Log("Right");

            currentAttackAnimationClips = attackAnimations.rightAttacks;

            Debug.Log("right direction" + currentAttackAnimationClips);
        }
    }

    private void AttackCoolDown()
    {
        timer -= 1f * Time.deltaTime;
        if(timer <= 0)
        {
            ChooseAttackAnimation();
            anim.SetBool("ATTACK", true);
            timer = currentPhase.coolDownTimer;
        }
    }

    private void ChooseAttackAnimation()
    {
        animationIndex = Random.Range(0, currentAttackAnimationClips.Count);
        animatorOverrideController["ATTACK"] = currentAttackAnimationClips[animationIndex];
        timer = currentPhase.coolDownTimer;
        anim.SetBool("ATTACK", false);
    }
}
