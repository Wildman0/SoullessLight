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

public enum PlayerActions
{
    Moving,
    Rolling,
    Sprinting,
    Attacking,
    Blocking,
    Invincible
}

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public PlayerMovement playerMovement;
    public CharacterController characterController;

    public delegate void SetPlayerStateHandler(PlayerActions index, bool b);
    public event SetPlayerStateHandler SetPlayerState;
    
    public bool[] playerState = new bool[Enum.GetNames(typeof(PlayerActions)).Length];
    
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
    
    
    public void OnSetPlayerState(PlayerActions index, bool b)
    {
        playerState[(int) index] = b;
    }

    public bool GetPlayerState(PlayerActions index)
    {
        return playerState[(int) index];
    }
}