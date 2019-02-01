using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCombo : MonoBehaviour
{
    Phase phase;

    public int animationIndex;
    public int lastIndex;

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
            if (lastIndex == animationIndex)
            {
                SelectNewIndex();
            }
            else
            {
                phase.animatorOverrideController["ATTACK"] = phase.currentAttackAnimationClips[animationIndex];
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
        lastIndex = animationIndex;
        Phase.isAttacking = false;
    }

    private void SelectNewIndex()
    {
        animationIndex = Random.Range(0, phase.currentAttackAnimationClips.Count);
    }
}
