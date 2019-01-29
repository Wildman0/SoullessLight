using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCombo : MonoBehaviour
{
    Phase phase;

    private int animationIndex;
    private int lastIndex;

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
        else
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
        SelectComboAnimations();//TODO: STACKOVERFLOW EXCEPTION HERE (FPS ISSUE)        HAPPENS WHEN PLAYER IS LOW ON HEALTH
    }
}
