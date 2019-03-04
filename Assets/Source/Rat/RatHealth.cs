using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatHealth : MonoBehaviour
{
    AnimHandler animHandler;

    [Range(0, 1)]
    public float health;

    private void Start()
    {
        animHandler = GetComponent<AnimHandler>();
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
            animHandler.DeathAnimation();
        }
    }
}
