﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RatHealth : MonoBehaviour
{
    private AnimController animController;
    public GameObject disableOnDeath;
    private CapsuleCollider cC;
    private BoxCollider bC;
    private Image healthUI;

    [Range(0, 1)]
    public float health;
    private float beginningHealth;

    public bool isDead;

    private void Start()
    {
        animController = GetComponentInChildren<AnimController>();

        cC = GetComponent < CapsuleCollider >();
        bC = GetComponent<BoxCollider>();

        healthUI = GetComponentInChildren<Image>();
        healthUI.enabled = false;

        beginningHealth = health;
    }

    void Update ()
    {
        Death();
        HealthUI();
	}

    private void HealthUI()
    {
        if (health < beginningHealth)
        {
            healthUI.enabled = true;
        }

        if (health >= 0.02f)
        {
            healthUI.fillAmount = health;
        }
    }

    private void Death()
    {
        if(health <= 0.02f)
        {
            isDead = true;
            healthUI.fillAmount = 0;

            animController.IsDead();
            disableOnDeath.active = false;
            cC.enabled = false;
            bC.enabled = false;
            healthUI.enabled = false;
        }

    }
}
