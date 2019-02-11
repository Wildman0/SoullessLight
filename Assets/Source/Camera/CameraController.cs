using System.Collections;
using System.Collections.Generic;
using NDA.PlayerInput;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;
    
    public Transform playerEmpty;
    public Transform target;
    public Transform secondaryTarget;

    public float smoothing = 0.125f;
    public Vector3 offset;
    private Vector3 originalOffset;
    private Vector3 desiredPosition = Vector3.zero;

    float verticalSens = 4.0f;
    private float horizontalSens = 4.0f;
    
    public float maxCamHeight = 0f;
    public float minCamHeight = -1.5f;

    public bool isLocked = false;
    public Animator LockOn;

    public Camera currentCamera;

    private void Awake()
    {
        if (!instance)
            instance = this;
        else
            Debug.LogError("More than one instance of CameraController in the scene");

        originalOffset = offset;
        currentCamera = gameObject.GetComponent<Camera>();
    }

    void LateUpdate ()
    {
        CheckForCameraLockToggle();
        
        //Makes the camera look at the midpoint of the player and the boss
        transform.LookAt(Midpoint(target, secondaryTarget));

        MoveCamera();
        ImposeVerticalCameraLimits();
    }

    void CheckForCameraLockToggle()
    {
        if (PlayerController.instance.inputController.cameraLockToggle)
            ToggleCameraLock();
    }

    void ToggleCameraLock()
    {
        offset = originalOffset;
        GameObject.Find("CameraEmpty").transform.rotation = Quaternion.Euler(0, 0, 0);
        isLocked = !isLocked;
        LockOn.SetTrigger("LockOn");
        
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
        if (isLocked)
            LockedCameraMovement();
        else
            UnlockedCameraMovement();
    }

    void LockedCameraMovement()
    {
        desiredPosition = (playerEmpty.position + playerEmpty.TransformDirection(offset));
        
        desiredPosition = ApplyVerticalCameraMovement(desiredPosition);
        
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothing);
        transform.position = smoothedPosition;

        offset = new Vector3(offset.x, offset.y, offset.z);
    }

    void UnlockedCameraMovement()
    {
        desiredPosition = Quaternion.AngleAxis (PlayerController.instance.inputController.rotateCamera * horizontalSens, Vector3.up) * offset;
        
        desiredPosition = ApplyVerticalCameraMovement(desiredPosition);

        //TODO: ISN'T CURRENT SMOOTHED, COME BACK TO AND CHANGE LATER
        Vector3 smoothedPosition = Vector3.Lerp(Vector3.zero, desiredPosition, 0.1f);
        
        offset = desiredPosition;
            
        transform.position = playerEmpty.position + playerEmpty.TransformDirection(desiredPosition);
        transform.LookAt(playerEmpty.position);
    }

    Vector3 ApplyVerticalCameraMovement(Vector3 v)
    {
        float f = verticalSens * (GameManager.inputController.raiseCamera -
                                  GameManager.inputController.lowerCamera) *
                  Time.fixedDeltaTime;

        return new Vector3(v.x, v.y += f, v.z);
    }
    
    //Returns the midpoint between the boss and the player to feed to the camera
    Vector3 Midpoint(Transform pointA, Transform pointB)
    {
        return new Vector3((pointA.position.x + pointB.position.x) / 2,
                           (pointA.position.y + pointB.position.y) / 2,
                           (pointA.position.z + pointB.position.z) / 2);
    }
}
