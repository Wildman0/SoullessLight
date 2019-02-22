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
    public Image playerStaminaBar;
    public Image bossHealthBar;
    public Image bossAttacked;
    public Image playerAttacked;
    public Image BossDefeated;

    public Image[] orbHealth;

    public Color playerHealthBarFull = Color.red;
    public Color playerHealthBarEmpty = new Color(120, 50, 50);

    public Color playerStaminaBarFull = Color.green;
    public Color playerStaminaBarEmpty;

    public Color healCoolDownDone = Color.yellow;
    public Color healCoolDownStart = new Color(58, 98, 100, 22);

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
    }

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

        if (f < PlayerHealth.instance.health + 0.01f &&
            Mathf.Abs(PlayerHealth.instance.health - playerHealthBar.fillAmount) > 0.015f)
        {
            f += 0.01f;
        }
        else if (f > PlayerHealth.instance.health &&
                 Mathf.Abs(PlayerHealth.instance.health - playerHealthBar.fillAmount) > 0.015f)
        {
            f -= 0.01f;
        }

        playerHealthBar.fillAmount = f;
        
        playerHealthBar.color = Color.Lerp(playerHealthBarEmpty,
                                           playerHealthBarFull,
                                           PlayerHealth.instance.health);
    }

    private void PlayerStaminaBar()
    {
        playerStaminaBar.fillAmount = PlayerStamina.instance.stamina;
        playerStaminaBar.color = Color.Lerp(playerStaminaBarEmpty,
                                            playerStaminaBarFull,
                                            PlayerStamina.instance.stamina);
    }

    private void BossHealthBar()
    {
        bossHealthBar.fillAmount = bossHealth.health;
        bossHealthBar.color = Color.Lerp(playerHealthBarEmpty,
                                         playerHealthBarFull,
                                         PlayerHealth.instance.health);
    }

    private void OrbHealth()
    {
        for (int i = 0; i < orbHealth.Length; i++)
        {
            orbHealth[i].fillAmount = OrbSetUp.orbHealth[i];
        }
    }
}
