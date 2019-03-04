using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimHandler : MonoBehaviour
{
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void IdleAnimation()
    {
        anim.SetBool("isIdle", true);

        anim.SetBool("isWalking", false);
    }

    public void WalkAnimation()
    {
        anim.SetBool("isWalking", true);

        anim.SetBool("isIdle", false);
    }

    public void AttackAnimation()
    {
        anim.SetTrigger("isHitting");

        anim.SetBool("isIdle", false);
        anim.SetBool("isWalking", false);
    }

    public void ChaseAnimation()
    {
        anim.SetBool("isWalking", true);

        anim.SetBool("isIdle", false);
    }

    public void DeathAnimation()
    {
        anim.SetTrigger("isDead");

        anim.SetBool("isIdle", false);
        anim.SetBool("isWalking", false);
    }
}
