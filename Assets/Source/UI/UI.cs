using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class UI : MonoBehaviour
{
    public static UI instance;
    
    private BossHealth bossHealth;

	private Canvas canvas;
    public Image deathImage;
    public Image Healing;
    public Image StaminaLow;

    public Image playerHealthBar;
    public Image playerHealthBarBehind;
    public Image playerStaminaBar;
    public Image playerStaminaBarBehind;
    public Image bossHealthBar;
    public Image bossAttacked;
    public Image playerAttacked;
    public Image BossDefeated;
    public Image OrbHurt;

    public Image[] orbHealth;
    public Image[] orbHealth1;

    [SerializeField] Image[] playerHealCharges;

    public Color playerHealthBarFull = Color.red;
    public Color playerHealthBarEmpty = new Color(120, 50, 50);

    public Color playerStaminaBarFull = Color.green;
    public Color playerStaminaBarEmpty;

    public Color healCoolDownDone = Color.yellow;
    public Color healCoolDownStart = new Color(58, 98, 100, 22);

    private float virtualHealth;
    private float virtualStamina;

    /// <summary>
    /// Sets a given image to a given condition
    /// </summary>
    /// <param name="image">The image you want to change the state of</param>
    /// <param name="condition">Whether or not you want to set this image to enabled or disabled</param>
    public static void SetImageActive(Image image, bool condition = true)
    {
        image.GetComponent<Image>().enabled = condition;
    }

    private void Start()
    {
        if (!instance)
            instance = this;
        else
            Debug.LogError("More than one instance of UI in the scene");
        
        bossHealth = GameObject.FindGameObjectWithTag("Boss").GetComponent<BossHealth>();
        playerHealthBarBehind.fillAmount = PlayerHealth.instance.health;

        //Setting Up Virtual Modifiers
        virtualHealth = 1f;
        virtualStamina = 1f;
    }

    //Runs every frame
    public void Update()
    {
        BossHealthBar();
        PlayerHealthBar();
        PlayerStaminaBar();
        OrbHealth();
    }
    
    //TODO: TEMPORARILY CHANGED TO MAKE THE HEALTH SMOOTHED
    private void PlayerHealthBar()
    {
        playerHealthBar.fillAmount = PlayerHealth.instance.health;
        

        float f = playerHealthBar.fillAmount;
        //float v = playerHealthBarBehind.fillAmount;

        if (f < PlayerHealth.instance.health + 0.01f &&
            Mathf.Abs(PlayerHealth.instance.health - playerHealthBar.fillAmount) > 0.015f)
        {
            f += 0.01f * Time.fixedDeltaTime;
        }
        else if (f > PlayerHealth.instance.health &&
                 Mathf.Abs(PlayerHealth.instance.health - playerHealthBar.fillAmount) > 0.015f)
        {
            f -= 0.01f;
           
        }

        playerHealthBar.fillAmount = f;
        playerHealthBarBehind.fillAmount = virtualHealth;
        
        
        playerHealthBar.color = Color.Lerp(playerHealthBarEmpty,
                                           playerHealthBarFull,
                                           PlayerHealth.instance.health);
        //VirtualHealth Smoothing
        if (virtualHealth > PlayerHealth.instance.health)
        {
            virtualHealth -= 0.05f * Time.deltaTime * 2f;
            Debug.Log(virtualHealth);
        }
        if (virtualHealth < PlayerHealth.instance.health)
        {
            virtualHealth = PlayerHealth.instance.health;
            Debug.Log(virtualHealth);
        }
    }

    //Manages the fill level of the stamina UI bar
    private void PlayerStaminaBar()
    {
        playerStaminaBar.fillAmount = PlayerStamina.instance.stamina;
        playerStaminaBar.color = Color.Lerp(playerStaminaBarEmpty,
                                            playerStaminaBarFull,
                                            PlayerStamina.instance.stamina);

        playerStaminaBarBehind.fillAmount = virtualStamina;

        //Apologies for messyness, I tried using "isUsingStaminaAction" but it ignored 
        if (!PlayerController.instance.GetPlayerState(PlayerActions.Attacking) &&
            (!PlayerController.instance.GetPlayerState(PlayerActions.Rolling)) &&
            (!PlayerController.instance.GetPlayerState(PlayerActions.Sprinting)))
            //Virtual Stamina Smoothing 
        {
            if (virtualStamina > PlayerStamina.instance.stamina)
            {
                virtualStamina -= 0.05f * Time.deltaTime * 5f;
            }
       
        }

        if (virtualStamina < PlayerStamina.instance.stamina)
        {
            virtualStamina = PlayerStamina.instance.stamina;
        }
    }

    //Manages the fill level of the boss health UI bar
    private void BossHealthBar()
    {
        bossHealthBar.fillAmount = bossHealth.health;
        bossHealthBar.color = Color.Lerp(playerHealthBarEmpty,
                                         playerHealthBarFull,
                                         PlayerHealth.instance.health);
    }

    private void OrbHealth()
    {
        orbHealth[0].fillAmount = OrbSetUp.health;
        orbHealth1[0].fillAmount = TutorialProjectile.health;
    }

    //Manages the heal charge UI elements
    public void SetPlayerHealthChargeCount(int count)
    {
        for (int i = count; i < playerHealCharges.Length; i++)
        {
            playerHealCharges[i].enabled = false;
        }
    }
}
