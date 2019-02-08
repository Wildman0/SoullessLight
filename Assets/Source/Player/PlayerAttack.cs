using System.Collections;
using System.Collections.Generic;
using NDA.FloatUtil;
using NDA.PlayerInput;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public static PlayerAttack instance;
    
    private bool canAttack = true;
    
    private GameObject bossGameObject;
    private BossHealth bossHealth;

    public float lightAttackDamage = 0.02f;
    [SerializeField] private float lightAttackMovementLockTime = 0.4f;
    [SerializeField] private float lightAttackStamina = 0.05f;
    
    public float heavyAttackDamage = 0.04f;
    [SerializeField] private float heavyAttackMovementLockTime = 2.0f;
    [SerializeField] private float heavyAttackStamina = 0.15f;
    
    
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
        if (FloatCasting.ToBool(PlayerController.instance.inputController.lightAttackDown) && canAttack &&
            PlayerStamina.instance.stamina > lightAttackStamina)
        {
            LightAttack();
        }
        else if (FloatCasting.ToBool(PlayerController.instance.inputController.heavyAttackDown) && canAttack &&
                 PlayerStamina.instance.stamina > heavyAttackStamina)
        {
            HeavyAttack();
        }
    }

    //Performs a light attack
    public void LightAttack()
    {
        StartCoroutine(LightAttackIEnumerator());
    }

    private IEnumerator LightAttackIEnumerator()
    {
        hitReg.ToggleHitreg(HitReg.PlayerAttackTypes.LightAttack);
        PlayerAnim.instance.LightAttack();

        PlayerStamina.instance.stamina -= lightAttackStamina;
        
        PlayerStamina.instance.isUsingStaminaAction = true;
        canAttack = false;
        yield return new WaitForSeconds(lightAttackMovementLockTime);
        canAttack = true;
        PlayerStamina.instance.isUsingStaminaAction = false;
    }

    public void HeavyAttack()
    {
        StartCoroutine(HeavyAttackIEnum());
    }

    private IEnumerator HeavyAttackIEnum()
    {
        hitReg.ToggleHitreg(HitReg.PlayerAttackTypes.HeavyAttack);
        PlayerAnim.instance.HeavyAttack();

        PlayerController.instance.playerState[(int)PlayerActions.HeavyAttacking] = true;
        PlayerMovement.instance.LockMovement(heavyAttackMovementLockTime);
        
        PlayerStamina.instance.stamina -= heavyAttackStamina;
        
        PlayerStamina.instance.isUsingStaminaAction = true;
        canAttack = false;
        yield return new WaitForSeconds(heavyAttackMovementLockTime);
        canAttack = true;
        PlayerStamina.instance.isUsingStaminaAction = false;
        
        PlayerController.instance.playerState[(int)PlayerActions.HeavyAttacking] = false;
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
