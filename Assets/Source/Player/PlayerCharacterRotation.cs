using System.Collections;
using System.Collections.Generic;
using NDA.FloatUtil;
using UnityEngine;

public class PlayerCharacterRotation : MonoBehaviour
{
    private PlayerController playerController;

    void Awake()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    void FixedUpdate()
    {
        //If the player isn't moving (Uses some large number as a check because this
        //Occasionally returns the player position when the player isn't moving
        if (Vector3.Distance(transform.position, playerController.CharacterModelTargetLookPosition()) > 500)
        {
            transform.LookAt(
                (playerController.CharacterModelTargetLookPosition()));
        }
    }
}
