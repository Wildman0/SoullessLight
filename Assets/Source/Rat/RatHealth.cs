using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatHealth : MonoBehaviour
{
    private AnimController animController;
    public GameObject disableOnDeath;
    private CapsuleCollider cC;

    [Range(0, 1)]
    public float health;

    private void Start()
    {
        animController = GetComponentInChildren<AnimController>();

        cC = GetComponent < CapsuleCollider >();
    }

    void Update ()
    {
        Death();
	}

    public void Damage()
    {

    }

    private void Death()
    {
        if(health <= 0)
        {
            animController.IsDead();
            disableOnDeath.active = false;
            cC.enabled = false;
        }

    }
}
