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
    
    public CharacterController characterController;

    //Passive bools
    public bool isGrounded { get; private set; }
    public bool isInvincible { get; private set; }
    public bool isMoving { get; private set; }
    public bool isRolling { get; private set; }
    public bool isAscending { get; private set; }
    public bool isDescending { get; private set; }

    public bool canAttack = true;
    public bool isSprinting;
    public bool isBlocking;

    //Diagonal movement bools
    private bool zIsNegative;
    private bool xIsNegative;

    //Movement modifiers
    private float maxSpeed = 2f;
    private float moveSpeed = 2f;
    private float speedIncreaseModifier = 15f;
    private float speedDecreaseModifier = 15f;
    private float rollSpeed = 3.8f;
    private float rollTime = 0.3f; 
    private float rollStaminaCost = 0.25f;
    private float deadZone = 1f;
    private float sprintSpeed = 4f;
    private float sprintStaminaCostPerSecond = 0.25f;

    private float zMoveRatio;
    private float xMoveRatio;
    private float ratioTotal;
    private float ratioModifier;

    public float stamina = 1f;
    private float staminaReplenishPerSecond = 0.25f;
    private float maxStamina = 1f;

    //Movement speeds in each axis
    private float moveSpeedX;
    private float moveSpeedNegX;
    private float moveSpeedZ;
    private float moveSpeedNegZ;
    public float totalMoveSpeedX;
    public float totalMoveSpeedZ;

    //Floats used as modifiers for smooth movement
    readonly float moveSmoothingModifier = 0.0f;
    readonly float moveSmoothingModifierUpperLimit = 1.0f;

    private int movementDisablers;

    //Audio
    public AudioSource audioSource;
    public AudioSource audioSource1;
    public AudioSource audioSource2;

    //Animation
    public PlayerAnim playerAnim;

    //Vector3s
    private Vector3 positionLastFrame;
    private Vector3 lastMovementInput;
    public Vector3 movement;
    public Vector3 targetLookPosition;

    private Vector2 moveRatio;

    //InputControllers
    public InputController inputController;
    
    //Heartbeat effect when near dying (Isaac)
    public GameObject NearDeath;

    private PlayerAttack playerAttack;

    public UI ui;

    public float totalMoveSpeed;

    public Camera mainCamera;

    /// <summary>
    /// Runs on initialization
    /// </summary>
    private void Start()
    {
        mainCamera = Camera.main;
        characterController = gameObject.GetComponent<CharacterController>();
        inputController = GameManager.inputController;
        playerAttack = GetComponentInChildren<PlayerAttack>();
        ui = GameObject.FindGameObjectWithTag("Canvas").GetComponent<UI>();
    }

    /// <summary>
    /// Replenishes player stamina by staminaReplenishPerSecond each second
    /// </summary>
    private void ReplenishStamina()
    {
        if (!isRolling &&
            !isSprinting)
        {
            stamina += (staminaReplenishPerSecond * Time.fixedDeltaTime);
        }

        //Limits stamina
        if (stamina > maxStamina)
        {
            stamina = maxStamina;
        }
    }
    
    /// <summary>
    /// Runs InputManager updates for the input method being used
    /// </summary>
    private void RunInputControllerUpdates()
    {
        inputController.Update();  
    }
    
    //TODO: EXPAND THIS
    //TODO: ADD STAMINA LIMITING
    /// <summary>
    /// Checks if the player has given any attack inputs and if so, performs the given attack
    /// </summary>
    void AttackChecks()
    {
        if (FloatCasting.ToBool(inputController.lightAttackDown) && canAttack)
            playerAttack.LightAttack();
    }

    /// <summary>
    /// Call whenever the conditions for player death have been met
    /// </summary>
    public void Death()
    {
        playerAnim.Death();
        DisableMovement(100);
        isInvincible = true;
        ui.deathImage.GetComponent<Image>().enabled = true;
        ui.deathImage.GetComponent<Animator>().SetTrigger("isEnd");

        StartCoroutine(DeathIEnumerator());
    }

    private IEnumerator DeathIEnumerator()
    {
        Time.timeScale = 0.5f;
        yield return new WaitForSeconds(1);
        audioSource2.Play(); //Music After Player Dies (Isaac)
        Time.timeScale = 1;
    }

    /// <summary>
    /// Sets passive bools through their individual methods
    /// </summary>
    private void SetPassiveBools()
    {
        isGrounded = PassiveBools.IsGrounded(this);
        isAscending = PassiveBools.IsAscending(this, positionLastFrame);
        isDescending = PassiveBools.IsDescending(this, positionLastFrame);
        isMoving = PassiveBools.IsMoving(this);
        
        isSprinting = FloatCasting.ToBool(inputController.sprint) && stamina > 0 && totalMoveSpeed > 4.5f;
        isBlocking = FloatCasting.ToBool(inputController.block);
    }

    public void DisableMovement(float time)
    {
        //EMPTY
    }

    public IEnumerator Retry()
    {
        yield return new WaitForSeconds(2);
       //going to add later
    }
        
    /// <summary>
    /// Runs all methods the need to run constantly (50 times per second)
    /// </summary>
    private void FixedUpdate()
    {
        SetPassiveBools();
        AttackChecks();
        RunInputControllerUpdates();
        ReplenishStamina();
    }
}