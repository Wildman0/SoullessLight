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
    public CharacterController characterController;

    //Passive bools
    public bool isGrounded { get; private set; }
    public bool isInvincible { get; private set; }
    public bool isMoving { get; private set; }
    public bool isRolling { get; private set; }
    public bool isAscending { get; private set; }
    public bool isDescending { get; private set; }

    public bool canRoll = true;
    public bool canAttack = true;
    //Added canHeal to because the old "CanHeal" meant that the heal trigger didn't have a cooldown (Isaac)
    public bool canHeal = true;
    public bool isSprinting;
    public bool isBlocking;

    //Diagonal movement bools
    private bool zIsNegative;
    private bool xIsNegative;

    public bool canMove = true;
    private bool healStop = false;


    //Movement modifiers
    private float maxSpeed = 2f;
    private float moveSpeed = 2f;
    private float currentMoveSpeed;
    private float speedIncreaseModifier = 15f;
    private float speedDecreaseModifier = 15f;
    private float rollSpeed = 3.8f;
    private float rollTime = 0.3f; 
    private float rollStaminaCost = 0.25f;
    private float deadZone = 1f;
    private float sprintSpeed = 4f;
    private float sprintStaminaCostPerSecond = 0.25f;
    private float healAmount = 0.2f;
    public float healTime = 2.2f;
    public float healCool = 3f;
    public float healCount = 3f;

    

    private float zMoveRatio;
    private float xMoveRatio;
    private float ratioTotal;
    private float ratioModifier;

    public float stamina = 1f;
    private float staminaReplenishPerSecond = 0.25f;
    private float maxStamina = 1f;
    public float health = 1f;
    private float HealTriggerDuration = 0.45f;

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

    //Input modifiers
    public float verticalSensitivity;
    public float horizontalSensitivity;

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
    private InputController inputController;
    private KeyboardInputController keyboardInputController = new KeyboardInputController();
    private XboxOneInputController xboxOneInputController = new XboxOneInputController();
    
    public GameObject boss;
    public GameObject playerCharacter;
    //Heartbeat effect when near dying (Isaac)
    public GameObject NearDeath;
    public GameObject targetCine;
    public GameObject targetJump;

    private PlayerAttack playerAttack;

    public UI ui;

    private const float fullHealth = 1.0f;

    public float totalMoveSpeed;

    private Camera mainCamera;

    /// <summary>
    /// Runs on initialization
    /// </summary>
    private void Start()
    {
        //TODO: TEMP FIX
        playerCharacter = transform.Find("Character2").gameObject;

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
    
    /// <summary>
    /// Gets the inputs from the relevant input controller and modifies them based on deadzone values
    /// </summary>
    private void GetInputs()
    {
        if (canMove)
        {
            moveSpeedX = MoveModifier(moveSpeedX, !FloatMath.IsZero(inputController.right));
            moveSpeedNegX = MoveModifier(moveSpeedNegX, !FloatMath.IsZero(inputController.left));
            moveSpeedZ = MoveModifier(moveSpeedZ, !FloatMath.IsZero(inputController.forward));
            moveSpeedNegZ = MoveModifier(moveSpeedNegZ, !FloatMath.IsZero(inputController.back));
        }
        else
        {
            moveSpeedX = moveSpeedNegX = moveSpeedNegZ = moveSpeedZ = 0;
        }

        RollInputCheck();
        HealInputCheck();
        DeadZoneChecks();
    }

    /// <summary>
    /// Checks if the roll key is being pressed and rolls if it is
    /// </summary>
    private void RollInputCheck()
    {
        if (FloatCasting.ToBool(inputController.rollDown) && canRoll && canHeal && totalMoveSpeed > 1) //Fixed rolling whilst not moving bug (Isaac)
        {
            Roll();
        }
    }

    private void HealInputCheck()
    {
        if (FloatCasting.ToBool(inputController.healDown) 
            && canHeal && !playerAnim.anim.GetBool("IsHealing") && (!isRolling) && !healStop && healCount > 0) //Fixed rolling whilst healing bug & Multiple heals bug (Isaac)
        {
            Heal();
            Debug.Log("Heal");
        }
    }

    /// <summary>
    /// If the player has enough stamina, rolls.
    /// </summary>
    private void Roll()
    {
        if (stamina >= rollStaminaCost)
        {
            StartCoroutine(RollTimer());
        }
        else if (stamina <= rollStaminaCost)
        {
            //TODO: PLAY ANIMATION WHERE PLAYER TRIES TO ROLL BUT CAN'T?
            ui.StaminaLow.GetComponent<Image>().enabled = true;
            ui.StaminaLow.GetComponent<Animator>().SetTrigger("IsStaminaLow");
        }
    }

    /// <summary>
    /// Sets relevant values and rolls
    /// </summary>
    /// <returns>null</returns>
    /// This is so you can still have rolling match the anim, whilst changing the invicibility (Isaac)
    IEnumerator RollTimer()
    {
        canRoll = false;
        stamina -= rollStaminaCost;
        isInvincible = true;
        Debug.Log("Is Rolling");
        isRolling = true;
        yield return new WaitForSeconds(0.4f); 
        isInvincible = false;
        yield return new WaitForSeconds(rollTime);
        isRolling = false;
        canRoll = true;
       
    }

    /// <summary>
    /// Sets the value for inputs to 0 if they're below a given deadzone threshold
    /// </summary>
    private void DeadZoneChecks()
    {
        moveSpeedX = SetToZeroIfBelowDeadZoneThreshold(moveSpeedX);
        moveSpeedNegX = SetToZeroIfBelowDeadZoneThreshold(moveSpeedNegX);
        moveSpeedZ = SetToZeroIfBelowDeadZoneThreshold(moveSpeedZ);
        moveSpeedNegZ = SetToZeroIfBelowDeadZoneThreshold(moveSpeedNegZ);
    }

    /// <summary>
    /// Returns 0 if f is below the deadzone threshold, otherwise returns the number as it was
    /// </summary>
    /// <param name="f">The number being checked against the deadzone threshold</param>
    /// <returns></returns>
    private float SetToZeroIfBelowDeadZoneThreshold(float f)
    {
        return (f > deadZone * (speedIncreaseModifier/10) ? f : 0);
    }

    /// <summary>
    /// Sets move speeds based on modified inputs
    /// </summary>
    private void SetMoveSpeeds()
    {
        moveSpeedX = (moveSpeedX > maxSpeed) ? maxSpeed : moveSpeedX;
        moveSpeedNegX = (moveSpeedNegX > maxSpeed) ? maxSpeed : moveSpeedNegX;
        moveSpeedZ = (moveSpeedZ > maxSpeed) ? maxSpeed : moveSpeedZ;
        moveSpeedNegZ = (moveSpeedNegZ > maxSpeed) ? maxSpeed : moveSpeedNegZ;

        totalMoveSpeedX = moveSpeedX - moveSpeedNegX;
        totalMoveSpeedZ = moveSpeedZ - moveSpeedNegZ;
    }

    /// <summary>
    /// Returns true if the player is moving diagonally, otherwise returns false
    /// </summary>
    /// <returns></returns>
    private bool IsMovingDiagonally()
    {
        return (!FloatMath.IsZero(totalMoveSpeedX) && !FloatMath.IsZero(totalMoveSpeedZ));
    }

    /// <summary>
    /// If the player is moving diagonally, this calculates the speeds they need to move on the
    /// X and Z axis in order to move at the ratio to the max speed they should be
    /// </summary>
    private void CalculateDiagonalMovement()
    {
        //Stores whether or not the player is moving negatively in either plane, as the math
        //has to be done using positive numbers, then converted back later
        zIsNegative = false;
        xIsNegative = false;

        InvertNegativeMovementValues();
        CalculateXZMovementRatio();

        //Sets the angle between side z (forward/back) and the hypotenuse (max moveSpeed)
        //Sets the angle between side x (right/left) and the hypotenuse (max moveSpeed)
        float zHypotenuseAngle = xMoveRatio * ratioModifier;
        float xHypotenuseAngle = zMoveRatio * ratioModifier;

        moveSpeedZ = Mathf.Sin(Mathf.Deg2Rad * zHypotenuseAngle) * maxSpeed;
        moveSpeedX = Mathf.Sin(Mathf.Deg2Rad * xHypotenuseAngle) * maxSpeed;

        //TODO: THIS FIXES BACKWARDS+RIGHT MOVEMENT BEING QUICKER BUT IS ONLY A TEMP FIX
        //TODO: AS IT IMMEDIATELY CANCELS OUT MOVEMENT (MAKING IT JAGGED)
        moveSpeedNegX = 0;
        moveSpeedNegZ = 0;

        UninvertNegativeMovementValues();
    }

    /// <summary>
    /// Inverts negative values for trigonometry calculations
    /// </summary>
    private void InvertNegativeMovementValues()
    {
        if (totalMoveSpeedX < 0)
        {
            totalMoveSpeedX = FloatMath.Invert(totalMoveSpeedX);
            xIsNegative = true;
        }

        if (totalMoveSpeedZ < 0)
        {
            totalMoveSpeedZ = FloatMath.Invert(totalMoveSpeedZ);
            zIsNegative = true;
        }
    }

    /// <summary>
    /// Uninverts negative values for trigonometry calculations
    /// </summary>
    private void UninvertNegativeMovementValues()
    {
        if (xIsNegative)
        {
            moveSpeedX = FloatMath.Invert(moveSpeedX);
            xIsNegative = false;
        }

        if (zIsNegative)
        {
            moveSpeedZ = FloatMath.Invert(moveSpeedZ);
            zIsNegative = false;
        }
    }

    /// <summary>
    /// Works out the ratio of xMovement to zMovement
    /// </summary>
    private void CalculateXZMovementRatio()
    {
        zMoveRatio = totalMoveSpeedX;
        xMoveRatio = totalMoveSpeedZ;
        ratioTotal = xMoveRatio + zMoveRatio;
        ratioModifier = 90 / ratioTotal;
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
    /// Returns the amount on each axis the player should move by this frame
    /// </summary>
    /// <returns></returns>
    private Vector3 MovementThisFrame()
    {
        return new Vector3
        (
            (moveSpeedX - moveSpeedNegX) * currentMoveSpeed 
                                         * MovingAxis(inputController.right, inputController.left)
                                         * Time.fixedDeltaTime,
            0,
            (moveSpeedZ - moveSpeedNegZ) * currentMoveSpeed
                                         * MovingAxis(inputController.forward, inputController.back)
                                         * Time.fixedDeltaTime
        );
    }

    /// <summary>
    /// Returns the position the player model should be looking at
    /// </summary>
    /// <returns></returns>
    public Vector3 CharacterModelTargetLookPosition()
    {
        Vector3 v = new Vector3
        (
            transform.position.x + (movement.x) * 2000000,
            playerCharacter.transform.position.y,
            transform.position.z + (movement.z) * 2000000
        ); 

        v = transform.TransformDirection(v);

        return v;
    }

    /// <summary>
    /// Returns the position the player should be looking at
    /// </summary>
    /// <returns></returns>
    public Vector3 TargetLookPosition()
    {
        return new Vector3
        (
            boss.transform.position.x,
            transform.position.y,
            boss.transform.position.z
        );
    }

    /// <summary>
    /// Returns a summary float of the movement on a given axis with 2 directions
    /// </summary>
    /// <param name="axis1"></param>
    /// <param name="axis2"></param>
    /// <returns></returns>
    private float MovingAxis(float axis1, float axis2)
    {
        if (FloatMath.IsZero(axis1))
            return axis2;
        else if (FloatMath.IsZero(axis2))
            return axis1;
        else
        {
            return FloatMath.InvertIfNegative(axis1 - axis2);
        }
    }

    /// <summary>
    /// Make the camera look towards the boss
    /// </summary>
    private void LookAtBoss()
    {
        transform.LookAt(targetLookPosition);
    }

    /// <summary>
    /// Makes the player character move by a given amount
    /// </summary>
    private void Move()
    {
        //transform.Translate(movement);
        
        characterController.SimpleMove(transform.TransformDirection(movement/Time.fixedDeltaTime));
    }


    /// <summary>
    /// Plays animation based on the current state(s) of the player
    /// </summary>
    //TODO: THIS METHOD IS WAY BEYOND ORIGINAL SCOPE, CHANGE THIS
    private void PlayAnims()
    {
        if (isRolling)
        {
            currentMoveSpeed = rollSpeed;
            playerAnim.Roll();
            Debug.Log("Roll");
        }
        else if (isSprinting && stamina > 0.001f)
        {
            currentMoveSpeed = sprintSpeed;
            stamina -= sprintStaminaCostPerSecond * Time.fixedDeltaTime;

            playerAnim.Run();
        }
        //TODO: THIS CONDITION JUST HAPPENS TO WORK, MAKE A CONDITION JUST FOR THIS CHECK
        else if (!canAttack)
        {
            currentMoveSpeed = 1.0f;
            //Debug.Log("Yeet");
        }
        else
        {
            currentMoveSpeed = moveSpeed;

            if (isMoving)
            {
                playerAnim.Walk();
            }
            else
            {
                playerAnim.Idle();
            }
        }
    }

    /// <summary>
    /// Returns a modified movespeed in order to smooth out moving when accelerating and decelerating 
    /// </summary>
    /// <param name="currentMoveModifier">The move modifier already in place</param>
    /// <param name="increasing">Whether or not the player's speed is increasing</param>
    /// <returns></returns>
    private float MoveModifier(float currentMoveModifier, bool increasing = true)
    {
        if (increasing)
        {
            return currentMoveModifier += speedIncreaseModifier;
        }
        else
        {
            if (currentMoveModifier < 0.1f && !increasing)
            {
                return 0;
            }

            return currentMoveModifier -= (speedDecreaseModifier/100);
        }
    }
    
    /// <summary>
    /// Damages the player by a given amount and checks if the player should be dead
    /// </summary>
    public void Damage(float i)
    {
        health -= i;

        if (health < 0.01f)
        {
            Death();
        }
        else
        {
            playerAnim.Flinch();
            DisableMovement(0.3f);
            mainCamera.GetComponent<RFX4_CameraShake>().PlayShake();
            Controller.Vibrate(0, 1, 0.3f);
        }
    }

    /// <summary>
    /// Call whenever the conditions for player death have been met
    /// </summary>
    private void Death()
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

    //TODO: NEEDS AN INVENTORY OF HEALING FLASKS
    //Returns whether or not the player can heal 
    //Ditched this, caused too many bugs, made canHeal Instead, just like how u did rolling (Isaac)
    private bool CanHeal()
    {
        if (health > 0)
            return true;
        else
            return false;
    }

    //Heals the player
    private void Heal()
    {
        if (canHeal && health > 0) 

            playerAnim.Heal();
            DisableMovement(healTime);
            StartCoroutine(HealDuration());
            ui.Healing.GetComponent<Image>().enabled = true;
            ui.Healing.GetComponent<Animator>().SetTrigger("IsDamaged");
            health += healAmount;
            healCount -= 1;
            Debug.Log(healCount);

        if (health > fullHealth)
        {
            health = fullHealth;
        }
    }
    //To fix multiple healing bug (Isaac)
    private IEnumerator HealDuration()
    {
        canHeal = false;
        yield return new WaitForSeconds(healTime);
        healStop = true;
        Debug.Log("Healing CoolDown");
        canHeal = true;
        yield return new WaitForSeconds(healCool);
        healStop = false;
        Debug.Log("Healing Ready"); 
    }

    public void DisableMovement(float time)
    {
        StartCoroutine(DisableMovementIEnumerator(time));
    }

    private IEnumerator DisableMovementIEnumerator(float time)
    {
        canMove = false;
        movementDisablers++;

        yield return new WaitForSeconds(time);
        movementDisablers--;

        if (movementDisablers == 0)
        {
            canMove = true;
        }
    }

    private IEnumerator Retry()
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
        GetInputs();
        SetMoveSpeeds();
        ReplenishStamina();

        if (health < 0.25f && !audioSource.isPlaying && health > 0f) //Heartbeat Effect Activate
        {
            audioSource.Play();
            NearDeath.GetComponent<Animator>().SetBool("NearDeath", true);
        }
        else if (health <= 0)
        {
            audioSource.Stop();
            audioSource1.Stop();
            StartCoroutine(Retry());

        }
        else if (health > 0.25f) //Heartbeat Effect Cancel (Isaac)
        {
            NearDeath.GetComponent<Animator>().SetBool("NearDeath", false);
        }

        if (IsMovingDiagonally())
        {
            CalculateDiagonalMovement();
        }

        PlayAnims();

        movement = MovementThisFrame();

        targetLookPosition = TargetLookPosition();

        Move();

        totalMoveSpeed = FloatMath.InvertIfNegative(movement.x)*100
                         + FloatMath.InvertIfNegative(movement.z)*100;

        //TODO: MOVE THIS
        playerAnim.Jog();

        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Cine")
        {
            Debug.Log("entered collider");
            if (FloatCasting.ToBool(inputController.jumpDown))
            {
                Debug.Log("A Pressed");
                StartCoroutine(Lerp());
                playerAnim.Cinematic();
                StartCoroutine(WaitSecs());
            }
        }
    }

    private IEnumerator WaitSecs()
    {
        yield return new WaitForSeconds(2.8f);
        StartCoroutine(Lerp1());               
    }

    private IEnumerator Lerp()
    {
        for (int i = 0; i < 30; i++)
        {
            GetComponent<CharacterController>().transform.position = Vector3.Lerp(transform.position, targetCine.transform.position, Time.deltaTime);
            yield return new WaitForFixedUpdate();
        }               
    }

    private IEnumerator Lerp1()
    {
        for (int i = 0; i < 30; i++)
        {
            GetComponent<CharacterController>().transform.position = Vector3.Lerp(transform.position, targetJump.transform.position, Time.deltaTime);
            yield return new WaitForFixedUpdate();
        }
    }
}