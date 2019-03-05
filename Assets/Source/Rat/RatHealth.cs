using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatHealth : MonoBehaviour
{

    [Range(0, 1)]
    public float health;

    private void Start()
    {

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
            
        }
    }
}
