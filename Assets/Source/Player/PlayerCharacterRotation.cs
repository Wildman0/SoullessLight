using System.Collections;
using System.Collections.Generic;
using NDA.FloatUtil;
using UnityEngine;

public class PlayerCharacterRotation : MonoBehaviour
{
    void Update()
    {
        if (!PlayerController.instance.playerState[(int) PlayerActions.Rolling])
        {
            //If the player isn't moving (Uses some large number as a check because this
            //Occasionally returns the player position when the player isn't moving
            if (Vector3.Distance(transform.position, PlayerMovement.instance.directionVector) > 500)
            {
                if (!PlayerMovement.instance.movementDisabled)
                {
                    if (CameraController.instance.isLocked)
                    {
                        //Smooth Rolling while lockedon[Isaac]
                        Vector3 direction = (PlayerMovement.instance.directionVector);
                        Quaternion lookRotation =
                            Quaternion.LookRotation(new Vector3(direction.x, direction.y, direction.z));
                        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 10f);

                        //transform.LookAt(PlayerMovement.instance.directionVector);
                    }
                    else
                    {
                        Smoothen();

                        if (!PlayerMovement.instance.movementLocked &&
                            !PlayerController.instance.GetPlayerState(PlayerActions.Rolling))
                        {
                            Transform t = CameraController.instance.currentCamera.transform;
                            t.eulerAngles = new Vector3(0, t.eulerAngles.y, t.eulerAngles.z);

                            Vector3 direction = t.TransformDirection(PlayerMovement.instance.directionVector);
                            Quaternion lookRotation =
                                Quaternion.LookRotation(new Vector3(direction.x, direction.y, direction.z));
                            transform.rotation =
                                Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);

                            //Isaac, Fixed the rolling bug with glitchy turning :)
                            //transform.LookAt(t.TransformDirection(PlayerMovement.instance.directionVector));
                            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, transform.eulerAngles.z);
                        }
                    }
                }
            }
        }
    }

    // Smoothen while rolling
    void Smoothen()
    {
        Transform t = CameraController.instance.currentCamera.transform;
        t.eulerAngles = new Vector3(0, t.eulerAngles.y, t.eulerAngles.z);

        Vector3 direction = (t.TransformDirection(PlayerMovement.instance.directionVector));
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, direction.y, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 15f);
    }
}
