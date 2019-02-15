using System.Collections;
using System.Collections.Generic;
using NDA.FloatUtil;
using UnityEngine;

public class PlayerCharacterRotation : MonoBehaviour
{
    void Update()
    {
        //If the player isn't moving (Uses some large number as a check because this
        //Occasionally returns the player position when the player isn't moving
        if (Vector3.Distance(transform.position, PlayerMovement.instance.directionVector) > 500)
        {
            if (!PlayerMovement.instance.movementDisabled)
            {
                if (CameraController.instance.isLocked)
                {
                    transform.LookAt(PlayerMovement.instance.directionVector);
                }
                else
                {
                    if (!PlayerMovement.instance.movementLocked)
                    {
                        Transform t = CameraController.instance.currentCamera.transform;
                        t.eulerAngles = new Vector3(0, t.eulerAngles.y, t.eulerAngles.z);

                        transform.LookAt(t.TransformDirection(PlayerMovement.instance.directionVector));
                        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, transform.eulerAngles.z);
                    }
                }
            }
        }
    }
}
