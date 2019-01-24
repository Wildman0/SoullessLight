using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCombo : MonoBehaviour
{
    Phase phase;

    public int index;
    public int lastIndex;

    private void Start()
    {
        phase = GetComponent<Phase>();
    }

    public void SelectComboAnimations()
    {
        if(phase.lastIndex == phase.animationIndex)
        {
            SelectNewIndex(); 
        }
        else
        {
            phase.animatorOverrideController["ATTACK"] = phase.currentAttackAnimationClips[phase.animationIndex];
            Phase.isAttacking = true;
            phase.anim.SetBool("Combo", true);

            StartCoroutine(Wait());
        }
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(1.4f);
        phase.anim.SetBool("Combo", false);
        phase.lastIndex = phase.animationIndex;
        Phase.isAttacking = false;
        phase.selectAttackStyle = false;
    }

    private void SelectNewIndex()
    {
        phase.animationIndex = Random.Range(0, phase.currentAttackAnimationClips.Count);
        SelectComboAnimations();
    }
}
