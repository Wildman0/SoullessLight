using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCombo : MonoBehaviour
{
    Phase phase;

    public bool selectAttack;

    private void Start()
    {
        phase = GetComponent<Phase>();
    }

    private void Update()
    {
        SelectComboAnimations();
    }

    public void SelectComboAnimations()
    {
        if (selectAttack == true)
        {
            Debug.Log("In");
            if (phase.lastIndex == phase.attackIndex)
            {
                phase.SelectNewIndex();
            }
            else
            {
                phase.animatorOverrideController["ATTACK"] = phase.currentAttackAnimationClips[phase.attackIndex].animationClip;
                Phase.isAttacking = true;
                phase.anim.SetBool("Combo", true);

                StartCoroutine(Wait());

                selectAttack = false;
            }
        }
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(1.4f);
        phase.anim.SetBool("Combo", false);
        phase.lastIndex = phase.attackIndex;
        Phase.isAttacking = false;
    }
}
