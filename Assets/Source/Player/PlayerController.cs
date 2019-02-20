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
    HeavyAttacking,
    Blocking,
    Invincible,
    Healing,
    InCinematic
}

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    
    public CharacterController characterController;

    public delegate void SetPlayerStateHandler(PlayerActions index, bool b);
    public event SetPlayerStateHandler SetPlayerState;
    
    [NonSerialized] public bool[] playerState = new bool[Enum.GetNames(typeof(PlayerActions)).Length];

    //Audio
    public AudioSource audioSource;
    public AudioSource audioSource1;
    public AudioSource audioSource2;

    //InputControllers
    public InputController inputController;
    
    public Camera mainCamera;

    private void Awake()
    {
        if (!instance)
            instance = this;
        else
            Debug.LogError("More than one instance of PlayerController in the scene");
    }

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