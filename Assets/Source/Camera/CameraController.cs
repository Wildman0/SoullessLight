using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform playerEmpty;
    public Transform target;
    public Transform secondaryTarget;

    public float smoothing = 0.125f;
    public Vector3 offset;

    float verticalSens = 1.75f;

    float maxCamHeight = 0f;
    float minCamHeight = -1.5f;
    
    void FixedUpdate () {
        //Makes the camera look at the midpoint of the player and the boss
        transform.LookAt(Midpoint(target, secondaryTarget));

        MoveCamera();
        ImposeVerticalCameraLimits();
    }

    //Stops the camera from going too high or too low
    void ImposeVerticalCameraLimits()
    {
        offset.y = (offset.y > 2f + maxCamHeight) ? 2f + maxCamHeight : offset.y;
        offset.y = (offset.y < 2f + minCamHeight) ? 2f + minCamHeight : offset.y;
    }

    //Moves the camera to the desired position
    void MoveCamera()
    {
        Vector3 desiredPosition = (playerEmpty.position + playerEmpty.TransformDirection(offset));
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothing);
        transform.position = smoothedPosition;

        float v = verticalSens * (GameManager.inputController.raiseCamera -
                                  GameManager.inputController.lowerCamera) *
                                  Time.fixedDeltaTime;

        offset = new Vector3(offset.x, offset.y + v, offset.z);
    }

    //Returns the midpoint between the boss and the player to feed to the camera
    Vector3 Midpoint(Transform pointA, Transform pointB)
    {
        return new Vector3((pointA.position.x + pointB.position.x) / 2,
                           (pointA.position.y + pointB.position.y) / 2,
                           (pointA.position.z + pointB.position.z) / 2);
    }
}
