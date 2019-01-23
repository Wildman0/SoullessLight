using System.Collections;
using System.Collections.Generic;
using NDA.FloatUtil;
using UnityEngine;

public class PlayerCharacterRotation : MonoBehaviour
{
    private PlayerMovement playerMovement;

    void Awake()
    {
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }

    void Update()
    {
        //If the player isn't moving (Uses some large number as a check because this
        //Occasionally returns the player position when the player isn't moving
        if (Vector3.Distance(transform.position, playerMovement.directionVector) > 500)
        {
            transform.LookAt(
                (playerMovement.directionVector));
        }
    }
}
