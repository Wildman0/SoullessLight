using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class UI : MonoBehaviour
{
    private BossHealth bossHealth;
    private PlayerController playerController;

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
    public Image healReady;

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
        bossHealth = GameObject.FindGameObjectWithTag("Boss").GetComponent<BossHealth>();
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    public void Update()
    {
        BossHealthBar();
        PlayerHealthBar();
        PlayerStaminaBar();
        healCoolDown();
    }
    
    //TODO: TEMPORARILY CHANGED TO MAKE THE HEALTH SMOOTHED
    private void PlayerHealthBar()
    {
        //playerHealthBar.fillAmount = playerController.health;

        float f = playerHealthBar.fillAmount;

        if (f < playerController.health + 0.01f &&
            Mathf.Abs(playerController.health - playerHealthBar.fillAmount) > 0.015f)
        {
            f += 0.01f;
        }
        else if (f > playerController.health &&
                 Mathf.Abs(playerController.health - playerHealthBar.fillAmount) > 0.015f)
        {
            f -= 0.01f;
        }

        playerHealthBar.fillAmount = f;
        
        playerHealthBar.color = Color.Lerp(playerHealthBarEmpty,
                                           playerHealthBarFull,
                                           playerController.health);
    }

    private void PlayerStaminaBar()
    {
        playerStaminaBar.fillAmount = playerController.stamina;
        playerStaminaBar.color = Color.Lerp(playerStaminaBarEmpty,
                                            playerStaminaBarFull,
                                            playerController.stamina);
    }

    private void BossHealthBar()
    {
        bossHealthBar.fillAmount = bossHealth.health;
        bossHealthBar.color = Color.Lerp(playerHealthBarEmpty,
                                         playerHealthBarFull,
                                         playerController.health);
    }

    private void healCoolDown()
    {
        healReady.fillAmount = playerController.healCount;
        healReady.color = Color.Lerp(healCoolDownStart,
                                     healCoolDownDone,
                                     playerController.healCount);
    }
}
