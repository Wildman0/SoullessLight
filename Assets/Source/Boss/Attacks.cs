using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacks : MonoBehaviour
{
    public static string[] closeAttacks;
    public static string[] longAttacks;
    public string currentAttack;

    public int index;
    public int lastIndex;
    public int comboIndex;

    public float comboChance;
    public static float coolDownTimer;
    public float countUpTimer;

    public static bool startAttacks;
    public bool selectAttack;
    public static bool isAttacking;
    public bool showIsAttacking;

    public void Update()
    {
        if (startAttacks == true)
        {
            DistanceCheck();
            CoolDownTimer(true);
        }
        else
        {
            CoolDownTimer(false);
        }

        showIsAttacking = isAttacking;
    }

    /// <summary>
    /// Checks to see what the distance is from boss to player
    /// </summary>
    private void DistanceCheck()
    {
        var distance = Distance.showDistance;
        if (distance == "Close")
        {
            CloseAttackManager();
        }
        else if (distance == "Long")
        {
            LongAttackManager();
        }
    }

    private void CloseAttackManager()
    {
        var phaseCheck = Boss.currentPhase;
        if(phaseCheck == "PhaseOne")
        {
            if(selectAttack == true)
            {
                comboChance = Random.Range(0.0f, 1.0f);
                if(comboChance < 0.9f)
                {
                    CloseAttack();
                }
                else
                {
                    CloseCombo();
                }
            }
        }
        else if(phaseCheck == "PhaseTwo")
        {
            if (selectAttack == true)
            {
                comboChance = Random.Range(0.0f, 1.0f);
                if (comboChance < 0.5f)
                {
                    CloseAttack();
                }
                else
                {
                    CloseCombo();
                }
            }
        }
        else if(phaseCheck == "PhaseThree")
        {
            if (selectAttack == true)
            {
                comboChance = Random.Range(0.0f, 1.0f);
                if (comboChance < 0.2f)
                {
                    CloseAttack();
                }
                else
                {
                    CloseCombo();
                }
            }
        }
    }

    /// <summary>
    /// if bool is true boss can select an close attack
    /// </summary>
    private void CloseAttack()
    {
        if (selectAttack == true)
        {
            if (lastIndex == index)
            {
                SelectNewCloseIndex();
            }
            else
            {
                BossAnim.anim.SetBool(currentAttack, true);
                selectAttack = false;

                StartCoroutine(DisableAnimBool());
                isAttacking = true;
            }
        }
    }

    private void CloseCombo()
    {
        if(selectAttack == true)
        {
            comboIndex = Random.Range(1, 8);
            BossAnim.anim.SetInteger("CloseCombo_Index", comboIndex);
            selectAttack = false;
            StartCoroutine(DisableAnimInt());
            isAttacking = true;
        }
    }

    /// <summary>
    /// Makes index choose another number
    /// </summary>
    private void SelectNewCloseIndex()
    {
        index = Random.Range(0, closeAttacks.Length);
        currentAttack = closeAttacks[index];
    }

    private void LongAttackManager()
    {
        var phaseCheck = Boss.currentPhase;
        if (phaseCheck == "PhaseOne")
        {
            if (selectAttack == true)
            {
                comboChance = Random.Range(0.0f, 1.0f);
                if (comboChance < 0.7f)
                {
                    LongAttack();
                }
                else
                {
                    LongCombo();
                }
            }
        }
        else if (phaseCheck == "PhaseTwo")
        {
            if (selectAttack == true)
            {
                comboChance = Random.Range(0.0f, 1.0f);
                if (comboChance < 0.5f)
                {
                    LongAttack();
                }
                else
                {
                    LongCombo();
                }
            }
        }
        else if (phaseCheck == "PhaseThree")
        {
            if (selectAttack == true)
            {
                comboChance = Random.Range(0.0f, 1.0f);
                if (comboChance < 0.5f)
                {
                    LongAttack();
                }
                else
                {
                    LongCombo();
                }
            }
        }
    }

    private void LongAttack()
    {
        if(selectAttack == true)
        {
            if(lastIndex == index)
            {
                SelectNewLongIndex();
            }
            else
            {
                BossAnim.anim.SetBool(currentAttack, true);
                selectAttack = false;

                StartCoroutine(DisableAnimBool());
            }
        }
    }

    private void LongCombo()
    {
        //if (selectAttack == true)
        //{
        //    comboIndex = Random.Range(1, 6);
        //    BossAnim.anim.SetInteger("LongCombo_Index", comboIndex);
        //    selectAttack = false;
        //    StartCoroutine(DisableAnimInt());
        //    isAttacking = true;
        //} 
    }

    private void SelectNewLongIndex()
    {
        index = Random.Range(0, longAttacks.Length);
        currentAttack = longAttacks[index];
    }

    /// <summary>
    /// After so many seconds turns off attack animation bool
    /// </summary>
    private IEnumerator DisableAnimBool()
    {
        yield return new WaitForSeconds(0.8f);
        BossAnim.anim.SetBool(currentAttack, false);
        lastIndex = index;
    }

    private IEnumerator DisableAnimInt()
    {
        yield return new WaitForSeconds(1.8f);
        BossAnim.anim.SetInteger("CloseCombo_Index", 0);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="timer"></param>
    private void CoolDownTimer(bool timer)
    {
        if (timer == true)
        {
            countUpTimer += 1f * Time.deltaTime;
            if (countUpTimer >= coolDownTimer)
            {
                selectAttack = true;
                countUpTimer = 0f;
            }
        }
        else
        {
            countUpTimer = 0f;
        }
    }
}
