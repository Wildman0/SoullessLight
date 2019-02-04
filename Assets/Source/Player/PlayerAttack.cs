using System.Collections;
using System.Collections.Generic;
using NDA.FloatUtil;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public static PlayerAttack instance;
    
    private bool canAttack = true;
    
    private GameObject bossGameObject;
    private BossHealth bossHealth;

    public float lightAttackDamage = 0.02f;
    private float lightAttackMovementLockTime = 0.4f;
    private float heavyAttackMovementLockTime = 0.6f;
    
    private AttackHitDetection attackHitDetection;
    public HitReg hitReg;

    private void Awake()
    {
        if (!instance)
            instance = this;
        else
            Debug.LogError("More than one instance of PlayerAttack");
    }

    private void Start()
    {
        SetBossValues();
        attackHitDetection = GetComponentInChildren<AttackHitDetection>();
        hitReg = gameObject.GetComponent<HitReg>();
    }

    void Update()
    {
        if (FloatCasting.ToBool(PlayerController.instance.inputController.lightAttackDown) && canAttack)
        {
            LightAttack();
        }
    }

    //Performs a light attack
    public void LightAttack()
    {
        StartCoroutine(LightAttackIEnumerator());
    }

    private IEnumerator LightAttackIEnumerator()
    {
        hitReg.ToggleHitreg();
        PlayerAnim.instance.LightAttack();
        
        canAttack = false;
        yield return new WaitForSeconds(lightAttackMovementLockTime);
        canAttack = true;
    }

    public void HeavyAttack()
    {
        
    }

    private IEnumerator HeavyAttackIEnum()
    {
        hitReg.ToggleHitreg();
        PlayerAnim.instance.HeavyAttack();

        canAttack = false;
        yield return new WaitForSeconds(heavyAttackMovementLockTime);
        canAttack = true;
    }

    //Takes a given amount of health away from the boss
    public void DamageBoss(float damage)
    {
        Debug.Log("Damage");
        bossHealth.Damage(damage); 
        Controller.Vibrate(0, 0.5f, 0.5f);
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
