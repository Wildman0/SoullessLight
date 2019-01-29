using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCombo : MonoBehaviour
{
    Phase phase;

    public int animationIndex;
    public int lastIndex;

    private void Start()
    {
        phase = GetComponent<Phase>();
    }

    public void SelectComboAnimations()
    {
        if(lastIndex == animationIndex)
        {
            SelectNewIndex();
        }
        else if (lastIndex != animationIndex)
        {
            Debug.Log("In");
            phase.animatorOverrideController["ATTACK"] = phase.currentAttackAnimationClips[animationIndex];
            Phase.isAttacking = true;
            phase.anim.SetBool("Combo", true);

            StartCoroutine(Wait());
        }
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(1.4f);
        phase.anim.SetBool("Combo", false);
        lastIndex = animationIndex;
        Phase.isAttacking = false;
        phase.selectAttackStyle = false;
    }

    private void SelectNewIndex()
    {
        animationIndex = Random.Range(0, phase.currentAttackAnimationClips.Count);

        phase.animatorOverrideController["ATTACK"] = phase.currentAttackAnimationClips[animationIndex];
        Phase.isAttacking = true;
        phase.anim.SetBool("Combo", true);

        StartCoroutine(Wait());
    }
}
