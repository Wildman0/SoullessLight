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
        
        currentCamera = gameObject.GetComponent<Camera>();
    }

    void Update ()
    {
        CheckForCameraLockToggle();
    }

    void CheckForCameraLockToggle()
    {
        if (PlayerController.instance.inputController.cameraLockToggle)
            ToggleCameraLock();
    }

    void ToggleCameraLock()
    {
        isLocked = !isLocked;
        LockOn.SetTrigger("LockOn");
    }
}
