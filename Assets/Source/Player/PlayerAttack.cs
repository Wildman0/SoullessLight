using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private bool canAttack = true;
    
    private GameObject bossGameObject;
    private BossHealth bossHealth;

    public float lightAttackDamage = 0.02f;
    private float lightAttackMovementLockTime = 0.4f;

    private AttackHitDetection attackHitDetection;
    private PlayerController playerController;
    public PlayerAnim playerAnim;
    public HitReg hitReg;
    
    //Runs on instantiation
    private void Start()
    {
        SetBossValues();
        attackHitDetection = GetComponentInChildren<AttackHitDetection>();
        playerController = GetComponentInParent<PlayerController>();
        hitReg = gameObject.GetComponent<HitReg>();
    }

    //Performs a light attack
    public void LightAttack()
    {
        StartCoroutine(LightAttackIEnumerator());
    }

    private IEnumerator LightAttackIEnumerator()
    {
        hitReg.ToggleHitreg();
        playerAnim.LightAttack();
        
        canAttack = false;
        yield return new WaitForSeconds(lightAttackMovementLockTime);
        canAttack = true;
    }

    //Takes a given amount of health away from the boss
    public void DamageBoss(float damage)
    {
        Debug.Log("Damage");
        bossHealth.Damage(damage); 
    }

    //Sets bossGameObject values, including checks for whether or not the Boss component is nested
    private void SetBossValues()
    {
        bossGameObject = bossGameObject ?? GameObject.FindGameObjectWithTag("Boss");

        if (bossGameObject.GetComponent<Boss>() == null)
        {
            bossHealth = bossGameObject.GetComponentInChildren<BossHealth>();
            
            if (bossHealth == null)
            {
                Debug.LogError("Was unable to find boss");
            }
        }
        else
        {
            bossHealth = bossGameObject.GetComponent<BossHealth>();

            if (bossHealth == null)
            {
                Debug.LogError("Was unable to find boss");
            }
        }

        bossGameObject = bossHealth.gameObject;
    }
}
