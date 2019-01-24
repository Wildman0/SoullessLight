using System;
using System.Collections;
using System.Collections.Generic;
using NDA.BoolUtil;
using UnityEngine;
using NDA.PlayerInput;
using NDA.FloatUtil;
using NDA.DebugUtil;
using UnityEngine.UI;
using XInputDotNetPure;

/// <summary>
/// Handles player input management and translates it into movement and rotation as well as
/// implementing animation methods defined in external classes where relevant.
/// Additionally, it provides some output fields to be used in debugging.
/// </summary>
[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public PlayerMovement playerMovement;
    public CharacterController characterController;

    public float stamina = 1f;
    private float staminaReplenishPerSecond = 0.25f;
    private float maxStamina = 1f;

    //Audio
    public AudioSource audioSource;
    public AudioSource audioSource1;
    public AudioSource audioSource2;

    //Animation
    public PlayerAnim playerAnim;

    //InputControllers
    public InputController inputController;
    
    public Camera mainCamera;

    /// <summary>
    /// Runs on initialization
    /// </summary>
    private void Start()
    {
        mainCamera = Camera.main;
        characterController = gameObject.GetComponent<CharacterController>();
        inputController = GameManager.inputController;
    }
    
    private void RunInputControllerUpdates()
    {
        inputController.Update();  
    }
    
    private void FixedUpdate()
    {
        RunInputControllerUpdates();
    }
}