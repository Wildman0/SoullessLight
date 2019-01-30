using System;
using System.Collections;
using System.Collections.Generic;
using NDA.FloatUtil;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	public static PlayerMovement instance;
	
	public delegate void SetPlayerStateHandler(PlayerActions index, bool b);
	public event SetPlayerStateHandler SetPlayerState;
	
	private int movementDisablers = 0;
	private bool movementDisabled;

	private const float directionVectorModifier = 10000f;
	
	private float currentMovementSpeed;
	private const float jogSpeed = 3.0f;
	private const float sprintSpeed = 4.5f;
	
	private const float gravity = 9.8f;
	private Vector3 movementTarget;

	private float rollTime = 1.0f;
	private float rollSpeed = 4.5f;
	
	public Vector3 directionVector;
	public float velocity;

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

		if (!PlayerController.instance.GetPlayerState(PlayerActions.Rolling))
			SetMovement();
		else
			SetRollMovement();
		
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
	
	private void SetMovement()
	{
		movementTarget = new Vector3(PlayerController.instance.inputController.right - PlayerController.instance.inputController.left,
			0,
			PlayerController.instance.inputController.forward - PlayerController.instance.inputController.back);

		movementTarget = transform.TransformDirection(movementTarget);
		
		SetDirectionVector();
		SetVelocity();
		
		SetPlayerState(PlayerActions.Moving, velocity > 0);
		SetPlayerState(PlayerActions.Sprinting,
			FloatCasting.ToBool(PlayerController.instance.inputController.sprint) && Math.Abs(PlayerStamina.instance.stamina) >= 0.01f);
	}

	void SetRollMovement()
	{
		movementTarget = directionVector;
	}

	float GetMovementSpeed()
	{
		if (PlayerController.instance.GetPlayerState(PlayerActions.Rolling))
			return rollSpeed;
		
		if (PlayerController.instance.GetPlayerState(PlayerActions.Sprinting))
			return sprintSpeed;
		
		return jogSpeed;
	}
	
	private void Move()
	{
		Vector3 v = Vector3.MoveTowards(Vector3.zero, movementTarget, GetMovementSpeed() * Time.deltaTime);
		
		PlayerController.instance.characterController.Move(v);
		
		PlayerAnim.instance.Jog();
	}

	private void SetVelocity()
	{
		velocity = (Mathf.Abs(directionVector.x) + Mathf.Abs(directionVector.z)) / directionVectorModifier * 30;
	}
	
	//Sets the direction the player is moving towards
	private void SetDirectionVector()
	{
		directionVector = movementTarget * directionVectorModifier;
	}

	private void ApplyGravity()
	{
		PlayerController.instance.characterController.Move(new Vector3(0, -gravity * Time.deltaTime, 0));
	}

	private void RollChecks()
	{
		if (!FloatMath.IsZero(PlayerController.instance.inputController.rollDown) &&
		    !PlayerController.instance.playerState[(int) PlayerActions.Rolling]
		    && PlayerStamina.instance.stamina > PlayerStamina.rollingStaminaReduction)
		{
			StartCoroutine(RollIEnum());
			PlayerStamina.instance.ReduceStamina(PlayerStamina.rollingStaminaReduction);
		}
	}
	
	private IEnumerator RollIEnum()
	{
		SetPlayerState(PlayerActions.Rolling, true);
		PlayerAnim.instance.Roll();
		yield return new WaitForSeconds(rollTime);
		SetPlayerState(PlayerActions.Rolling, false);
	}
	
	private void OnDestroy()
	{
		SetPlayerState -= PlayerController.instance.OnSetPlayerState;
	}
}
