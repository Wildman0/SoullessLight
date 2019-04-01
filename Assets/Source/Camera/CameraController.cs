using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using Cinemachine;
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

    void Start()
    {
        SetCinemachineAxes();
    }
    
    // Sets the axes that cinemachine uses for camera movement. Note that this is set up with controller by default so 
    // there is no need to change it to controller controls.
    void SetCinemachineAxes()
    {
        if (GameManager.instance.controller == InputController.ControllerType.Keyboard)
        {
            cineMachine.m_XAxis.m_InputAxisName = "Mouse X";
            cineMachine.m_YAxis.m_InputAxisName = "Mouse Y";
        }
    }

    void LateUpdate ()
    {
        CheckForCameraLockToggle();

        if (isLocked)
            LockedCameraMovement();
    }

    // Checks for camera lock toggle input
    void CheckForCameraLockToggle()
    {
        if (PlayerController.instance.inputController.cameraLockToggle)
            ToggleCameraLock();
    }

    // Toggles whether or not the camera is locked to a target or is free looking
    void ToggleCameraLock()
    {
        isLocked = !isLocked;
        LockOn.SetTrigger("LockOn");
        
        if (isLocked)
        {
            cinemachineBrain.enabled = false;
            secondaryTarget = FindNearestEnemy().transform;
        }
        else
        {
            cinemachineBrain.enabled = true;
            cineMachine.m_LookAt = playerEmpty;
        }
    }
    
    // Movement the camera while locked onto a target
    void LockedCameraMovement()	
    {	
        desiredPosition = (playerEmpty.position + playerEmpty.TransformDirection(offset));	

        desiredPosition = ApplyVerticalCameraMovement(desiredPosition);	

        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothing);	
        transform.position = smoothedPosition;	
        
        offset = new Vector3(offset.x, offset.y, offset.z);	
        
        transform.LookAt(secondaryTarget);
    }
    
    // Applies vertical camera movement according to relevant input
    Vector3 ApplyVerticalCameraMovement(Vector3 v)	
    {	
        float f = verticalSens * (GameManager.inputController.raiseCamera -	
                                  GameManager.inputController.lowerCamera) *	
                  Time.fixedDeltaTime;	

        return new Vector3(v.x, v.y += f, v.z);	
    }
    
    // Returns the midpoint between the boss and the player to feed to the camera	
    Vector3 Midpoint(Transform pointA, Transform pointB)	
    {	
        return new Vector3((pointA.position.x + pointB.position.x) / 2,	
            (pointA.position.y + pointB.position.y) / 2,	
            (pointA.position.z + pointB.position.z) / 2);	
    }

    // Returns the nearest boss/rat enemy to the camera
    GameObject FindNearestEnemy()
    {
        List<GameObject> go = new List<GameObject>();
        go.AddRange(GameObject.FindGameObjectsWithTag("Rat"));
        go.AddRange(GameObject.FindGameObjectsWithTag("Boss"));

        float[] distances = new float[go.Count];

        for (int i = 0; i < distances.Length; i++)
        {
            distances[i] = Vector3.Distance(gameObject.transform.position, go[i].transform.position);
        }

        return go[Array.IndexOf(distances, distances.Min())];
    }

    public void ShakeCamera(float intensity, float time)
    {
        StartCoroutine(ShakeCameraIEnumerator(intensity, time));
    }

    private IEnumerator ShakeCameraIEnumerator(float intensity, float time)
    {
        cineMachine.GetRig(1).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain =
            2.5f * intensity;
        cineMachine.GetRig(1).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain =
            1.0f * intensity;
        
        yield return new WaitForSeconds(time);
        
        cineMachine.GetRig(1).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = 0.0f;
        cineMachine.GetRig(1).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0.0f;
    }
}
