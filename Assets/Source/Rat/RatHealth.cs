using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatHealth : MonoBehaviour
{
    private AnimController animController;

    [Range(0, 1)]
    public float health;

    private void Start()
    {
        animController = GetComponentInChildren<AnimController>();
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
        }
    }
}
