using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimController : MonoBehaviour
{
    public Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void IsIdle()
    {
        anim.SetBool("isIdle", true);

        anim.SetBool("isWalking", false);
    }

    public void IsWalking()
    {
        anim.SetBool("isWalking", true);

        anim.SetBool("isIdle", false);
    }

    public void IsAttacking()
    {
        anim.SetTrigger("isHitting");

        anim.SetBool("isWalking", false);
        anim.SetBool("isIdle", false);
    }

    public void IsDead()
    {

    }
}
