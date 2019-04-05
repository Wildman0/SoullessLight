using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using NDA.FloatUtil;
using UnityEditor.Experimental.Rendering;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	public static PlayerMovement instance;
	
	public delegate void SetPlayerStateHandler(PlayerActions index, bool b);
	public event SetPlayerStateHandler SetPlayerState;
	
	private int movementDisablers = 0;
	public bool movementDisabled;

	private int movementLockers = 0;
	public bool movementLocked;
	
	private const float directionVectorModifier = 10000f;
	
	private float currentMovementSpeed;
	[SerializeField] private float jogSpeed = 3.0f;
	[SerializeField] private float sprintSpeed = 5.5f;

	[SerializeField] private float directionChangeSpeed = 0.15f;
	
	private const float gravity = 9.8f;
	private Vector3 movementTarget;
	private Vector3 movementVector;

	[SerializeField] private float rollTime = 1.0f;
	[SerializeField] private float rollInvincibilityTime = 0.4f;
	[SerializeField] private float rollSpeed = 4.5f;
	[SerializeField] private float additionalTimeBetweenRolls = 1.0f;
	
	[SerializeField] private float heavyAttackSpeed = 1.0f;
	
	public Vector3 directionVector;
	public float velocity;

	public Vector3 dirVector;
	
	private void Awake()
	{
		if (!instance)
			instance = this;
		else
			Debug.LogError("More than one instance of PlayerMovement");
	}

	void Start()
	{
		SetPlayerState += PlayerController.instance.OnSetPlayerState;
	}

	private void Update()
	{
		RollChecks();

		if (!movementLocked)
			SetMovement();
		
		Move();
		ApplyGravity();
	}

	//Checks for collision entering in order to play the jumpdown cinematic
	private void OnTriggerEnter(Collider coll)
	{
		if (coll.CompareTag("Cine"))
		{
			IntroCutscene();
			CinematicPusher.Trigger();
		}
	}

	//Disables movement for a given amount of time
	public void DisableMovement(float seconds)
	{
		StartCoroutine(DisableMovementIEnum(seconds));
	}

	private IEnumerator DisableMovementIEnum(float seconds)
	{
		movementDisabled = true;
		movementDisablers++;
		yield return new WaitForSeconds(seconds);
		movementDisablers--;

		if (movementDisablers < 1)
			movementDisabled = false;

	}

	//Locks the players movement direction and speed for a given amount of time
	public void LockMovement(float time)
	{
		StartCoroutine(LockMovementIEnum(time));
	}

	private IEnumerator LockMovementIEnum(float time)
	{
		movementLocked = true;
		movementLockers++;
		yield return new WaitForSeconds(time);
		movementLockers--;

		if (movementLockers < 1)
		{
			movementLocked = false;

			movementTarget = Vector3.zero;
		}
	}
	
	//Sets the player's movement direction and speed based on current circumstances and input
	private void SetMovement()
	{
		Vector3 vec = movementTarget;
		
		movementTarget = new Vector3(PlayerController.instance.inputController.right - PlayerController.instance.inputController.left,
			0,
			PlayerController.instance.inputController.forward - PlayerController.instance.inputController.back);
		
		movementTarget = Vector3.Lerp(vec, movementTarget, directionChangeSpeed);
		
		if (CameraController.instance.isLocked)
		{
			//TODO:TEMPORARY FIX TO LESSEN THE IMPACT OF THE ROLL MOVEMENT BUG
			if (Mathf.Abs(movementTarget.x) < 1.1)
			{
				movementVector = transform.TransformDirection(movementTarget);
				dirVector = movementVector;	
			}
		}
		else
		{
			//TODO: STOP THIS FROM BEING TIED DIRECTLY TO CAMERA (ONLY X AND Z)
			dirVector = transform.TransformDirection(movementTarget);
			movementVector = Camera.main.transform.TransformDirection(movementTarget);
		}

		//Clamps the magnitude of the vector, otherwise going too far in one direction for too long results in the 
		//character going in that way for a long time
		movementTarget = Vector3.ClampMagnitude(movementTarget, 2.0f);
		
		SetDirectionVector();
		SetVelocity();
		
		SetPlayerState(PlayerActions.Moving, velocity > 0);
		SetPlayerState(PlayerActions.Sprinting,
			FloatCasting.ToBool(PlayerController.instance.inputController.sprint) && CanSprint());
	}

	//Returns the speed the player should be moving at
	float GetMovementSpeed()
	{
		if (PlayerController.instance.GetPlayerState(PlayerActions.HeavyAttacking))
			return heavyAttackSpeed;
		
		if (PlayerController.instance.GetPlayerState(PlayerActions.Rolling))
			return rollSpeed;
		
		if (PlayerController.instance.GetPlayerState(PlayerActions.Sprinting))
			return sprintSpeed;

		return jogSpeed;
	}
	
	//Moves the character controller a given amount
	private void Move()
	{
		if (!movementDisabled)
		{
			Vector3 v = Vector3.MoveTowards(Vector3.zero, movementVector, GetMovementSpeed() * Time.deltaTime);

			PlayerController.instance.characterController.Move(v);

			if (PlayerController.instance.GetPlayerState(PlayerActions.Sprinting))
				PlayerAnim.instance.Run();
			else
				PlayerAnim.instance.Jog();
		}
	}

	//Sets the current velocity of the player
	private void SetVelocity()
	{
		velocity = (Mathf.Abs(directionVector.x) + Mathf.Abs(directionVector.z)) / directionVectorModifier * 30;
	}
	
	//Sets the direction the player is moving towards
	private void SetDirectionVector()
	{
		if (Mathf.Abs((dirVector * directionVectorModifier).magnitude) < 140000.0f)
			directionVector = dirVector * directionVectorModifier;
	}

	//Applies gravity to the character controller
	private void ApplyGravity()
	{
		PlayerController.instance.characterController.Move(new Vector3(0, -gravity * Time.deltaTime, 0));
	}

	//Returns whether or not the player is able to sprint based on their current action
	bool CanSprint()
	{
		return Math.Abs(PlayerStamina.instance.stamina) >= 0.01f && velocity > 5.0f 
            && !PlayerController.instance.GetPlayerState(PlayerActions.HeavyAttacking) 
            && !PlayerController.instance.GetPlayerState(PlayerActions.Attacking)
            && !PlayerController.instance.GetPlayerState(PlayerActions.Healing);
    }
	
	//Returns whether or not the player is able to roll based on their current action
	bool CanRoll()
	{
		return !FloatMath.IsZero(PlayerController.instance.inputController.rollDown)
		       && !PlayerController.instance.GetPlayerState(PlayerActions.Rolling)
		       && !PlayerController.instance.GetPlayerState(PlayerActions.Healing)
		       && !PlayerController.instance.GetPlayerState(PlayerActions.HeavyAttacking)
		       && !PlayerController.instance.GetPlayerState(PlayerActions.Sprinting)
		       && PlayerStamina.instance.stamina > PlayerStamina.instance.rollingStaminaReduction;
	}
	
	//Checks if the player is able to roll, if so then rolls
	private void RollChecks()
	{
		if (CanRoll())
		{
			StartCoroutine(RollIEnum());
			StartCoroutine(RollInvincibilityIEnum());
			PlayerStamina.instance.ReduceStamina(PlayerStamina.instance.rollingStaminaReduction);
		}
	}
	
	private IEnumerator RollIEnum()
	{
		SetPlayerState(PlayerActions.Rolling, true);
		PlayerAnim.instance.Roll();
		LockMovement(rollTime);
		yield return new WaitForSeconds(additionalTimeBetweenRolls);
		SetPlayerState(PlayerActions.Rolling, false);
	}

	//Makes the player invincible for a given amount of time when they roll
	private IEnumerator RollInvincibilityIEnum()
	{
		PlayerHealth.instance.isInvincible = true;
		yield return new WaitForSeconds(rollInvincibilityTime);
		PlayerHealth.instance.isInvincible = false;
	}
	
	//Unsubscribes from delegates whenever destroyed
	private void OnDestroy()
	{
		SetPlayerState -= PlayerController.instance.OnSetPlayerState;
	}

	private void IntroCutscene()
	{
		StartCoroutine(IntroCutSceneIEnum());
	}

	private IEnumerator IntroCutSceneIEnum()
	{
		SetPlayerState(PlayerActions.InCinematic, true);
		PlayerAnim.instance.Cinematic();
		DisableMovement(2.8f);
		yield return new WaitForSeconds(0.3f);
		movementVector = Vector3.forward;
		LockMovement(0.3f);
		SetPlayerState(PlayerActions.InCinematic, false);
	}
}
