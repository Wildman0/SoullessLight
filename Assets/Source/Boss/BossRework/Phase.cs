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
    private CoolDown coolDown;
    public Animator anim;

    public int animationIndex;
    public int lastIndex;

    public float comboChance;

    public bool activateAttacking;
    public bool intermission;

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
            currentPhase = phaseValues.Find(x => x.min <= bossHealth.health && x.max >= bossHealth.health);
            Debug.Log(currentPhase.phaseName);
            GetAttackAnimations();
            comboChance = currentPhase.comboChance;

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

    public void ChooseAttackAnimation()
    {
        //float randomNum = Random.Range(0.0f, 1.0f);
        //Debug.Log(randomNum);

        //if (randomNum < comboChance)
        {
            if (lastIndex == animationIndex)
            {
                SelectNewIndex();
            }
            else
            {
                animatorOverrideController["ATTACK"] = currentAttackAnimationClips[animationIndex];
                anim.SetBool("Attack", true);

                StartCoroutine(Wait());
            }
    }
        //else
        //{
        //    ComboChance();
        //}
    }

    public IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.6f);
        anim.SetBool("Attack", false);
        lastIndex = animationIndex;
    }

    private void SelectNewIndex()
    {
        animationIndex = Random.Range(0, currentAttackAnimationClips.Count);
        ChooseAttackAnimation();
    }

    //private void ComboChance()
    //{
    //    anim.SetBool("Combo", true);
    //    animatorOverrideController["ATTACK"] = currentAttackAnimationClips[animationIndex];
    //    StartCoroutine(test());
    //}

    private IEnumerator test()
    {
        yield return new WaitForSeconds(0.3f);
        anim.SetBool("Combo", false);
    }
}
