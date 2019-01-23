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
    public CoolDown coolDown;
    public Animator anim;

    public int animationIndex;
    public int lastIndex;

    public float comboChance;

    public bool activateAttacking;
    public bool intermission;
    public static bool isAttacking;

    private void Start()
    {
        anim = GetComponent<Animator>();
        anim.runtimeAnimatorController = animatorOverrideController;
        ////animatorOverrideController = new AnimatorOverrideController(anim.runtimeAnimatorController);
        distance = GetComponent<Distance>();
        coolDown = GetComponent<CoolDown>();
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
    }

    private void SelectPhase()
    {
        if(anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            currentPhase = phaseValues.Find(x => x.min >= bossHealth.health && x.max <= bossHealth.health);
            GetAttackAnimations();
            comboChance = currentPhase.comboChance;
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
        if (distance.showDesignersDistance == "Close")
        {
            attackAnimations = currentPhase.closeAttacks;
        }
        else if (distance.showDesignersDistance == "Mid")
        {
            attackAnimations = currentPhase.midAttacks;
        }
        else if (distance.showDesignersDistance == "Long")
        {
            attackAnimations = currentPhase.longAttacks;
        }

        if (PlayerDirection.direction == "Left")
        {
            currentAttackAnimationClips = attackAnimations.leftAttacks;
        }
        else if (PlayerDirection.direction == "Right")
        {
            currentAttackAnimationClips = attackAnimations.rightAttacks;
        }
    }

    public void ChooseAttackAnimation()
    {
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
    }

    public IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.6f);
        anim.SetBool("Attack", false);
        lastIndex = animationIndex;
        isAttacking = false;
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
