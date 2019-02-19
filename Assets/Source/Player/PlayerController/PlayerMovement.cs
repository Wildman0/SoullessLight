using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
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
	
	private void SetMovement()
	{
		Vector3 vec = movementTarget;
		
		movementTarget = new Vector3(PlayerController.instance.inputController.right - PlayerController.instance.inputController.left,
			0,
			PlayerController.instance.inputController.forward - PlayerController.instance.inputController.back);
		
		//Debug.Log(vec);
		//Debug.Log(movementTarget);
		
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
			FloatCasting.ToBool(PlayerController.instance.inputController.sprint) && Math.Abs(PlayerStamina.instance.stamina) >= 0.01f && PlayerMovement.instance.velocity > 5.0f);
	}

	float GetMovementSpeed()
	{
		if (PlayerController.instance.GetPlayerState(PlayerActions.Rolling))
			return rollSpeed;
		
		if (PlayerController.instance.GetPlayerState(PlayerActions.Sprinting))
			return sprintSpeed;

		if (PlayerController.instance.GetPlayerState(PlayerActions.HeavyAttacking))
			return heavyAttackSpeed;
			
		return jogSpeed;
	}
	
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

	private void ApplyGravity()
	{
		PlayerController.instance.characterController.Move(new Vector3(0, -gravity * Time.deltaTime, 0));
	}

	bool CanRoll()
	{
		return !FloatMath.IsZero(PlayerController.instance.inputController.rollDown)
		       && !PlayerController.instance.GetPlayerState(PlayerActions.Rolling)
		       && !PlayerController.instance.GetPlayerState(PlayerActions.Healing)
		       && !PlayerController.instance.GetPlayerState(PlayerActions.HeavyAttacking)
		       && PlayerStamina.instance.stamina > PlayerStamina.rollingStaminaReduction;
	}
	
	private void RollChecks()
	{
		if (CanRoll())
		{
			StartCoroutine(RollIEnum());
			StartCoroutine(RollInvincibilityIEnum());
			PlayerStamina.instance.ReduceStamina(PlayerStamina.rollingStaminaReduction);
		}
	}
	
	private IEnumerator RollIEnum()
	{
		SetPlayerState(PlayerActions.Rolling, true);
		PlayerAnim.instance.Roll();
		LockMovement(rollTime);
		yield return new WaitForSeconds(rollTime);
		SetPlayerState(PlayerActions.Rolling, false);
	}

	private IEnumerator RollInvincibilityIEnum()
	{
		PlayerHealth.instance.isInvincible = true;
		yield return new WaitForSeconds(rollInvincibilityTime);
		PlayerHealth.instance.isInvincible = false;
	}
	
	private void OnDestroy()
	{
		SetPlayerState -= PlayerController.instance.OnSetPlayerState;
	}
}
