using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using NDA.PlayerInput;
using UnityEditor.Experimental.Animations;
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
    [SerializeField] private CinemachineFreeLook cineMachine;
    private CinemachineBrain cinemachineBrain;

    private void Awake()
    {
        if (!instance)
            instance = this;
        else
            Debug.LogError("More than one instance of CameraController in the scene");
        
        currentCamera = gameObject.GetComponent<Camera>();
        cinemachineBrain = gameObject.GetComponent<CinemachineBrain>();
    }

    void LateUpdate ()
    {
        CheckForCameraLockToggle();

        if (isLocked)
            LockedCameraMovement();
    }

    //Checks for camera lock toggle input
    void CheckForCameraLockToggle()
    {
        if (PlayerController.instance.inputController.cameraLockToggle)
            ToggleCameraLock();
    }

    //Toggles whether or not the camera is locked to a target or is free looking
    void ToggleCameraLock()
    {
        isLocked = !isLocked;
        LockOn.SetTrigger("LockOn");

        if (isLocked)
        {
            cinemachineBrain.enabled = false;
        }
        else
        {
            cinemachineBrain.enabled = true;
            cineMachine.m_LookAt = playerEmpty;
        }
    }
    
    //Movement the camera while locked onto a target
    void LockedCameraMovement()	
    {	
        desiredPosition = (playerEmpty.position + playerEmpty.TransformDirection(offset));	

        desiredPosition = ApplyVerticalCameraMovement(desiredPosition);	

        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothing);	
        transform.position = smoothedPosition;	
        
        offset = new Vector3(offset.x, offset.y, offset.z);	
        
        transform.LookAt(secondaryTarget);
    }
    
    //Applies vertical camera movement according to relevant input
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
