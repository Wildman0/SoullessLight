using System;
using System.Collections;
using System.Collections.Generic;
using NDA.FloatUtil;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	public delegate void SetPlayerStateHandler(PlayerActions index, bool b);
	public event SetPlayerStateHandler SetPlayerState;
	
	public PlayerController playerController;
	
	private int movementDisablers = 0;
	private bool movementDisabled;

	private const float directionVectorModifier = 10000f;
	private float maxMoveDistance = 3.0f;
	private const float gravity = 9.8f;
	private Vector3 movementTarget;

	private float rollTime = 1.0f;
	private float rollSpeed = 4.5f;
	
	public Vector3 directionVector;
	public float velocity;

	private void Awake()
	{
		SetPlayerState += playerController.OnSetPlayerState;
	}

	private void Update()
	{
		RollChecks();

		if (!playerController.GetPlayerState(PlayerActions.Rolling))
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
		movementTarget = new Vector3(playerController.inputController.right - playerController.inputController.left,
			0,
			playerController.inputController.forward - playerController.inputController.back);

		movementTarget = transform.TransformDirection(movementTarget);
		SetDirectionVector();
		SetVelocity();
		SetPlayerState(PlayerActions.Moving, velocity > 0);
	}

	void SetRollMovement()
	{
		movementTarget = directionVector;
	}
	
	private void Move()
	{
		Vector3 v = Vector3.zero;
		
		if (!playerController.GetPlayerState(PlayerActions.Rolling))
			v = Vector3.MoveTowards(Vector3.zero, movementTarget, maxMoveDistance * Time.deltaTime);

		if (playerController.GetPlayerState(PlayerActions.Rolling))
			v = Vector3.MoveTowards(Vector3.zero, movementTarget, rollSpeed * Time.deltaTime);
			

		playerController.characterController.Move(v);
		
		playerController.playerAnim.Jog();
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
		playerController.characterController.Move(new Vector3(0, -gravity * Time.deltaTime, 0));
	}

	private void RollChecks()
	{
		if (!FloatMath.IsZero(playerController.inputController.rollDown) &&
		    !playerController.playerState[(int) PlayerActions.Rolling])
		{
			StartCoroutine(RollIEnum());
		}
	}
	
	private IEnumerator RollIEnum()
	{
		SetPlayerState(PlayerActions.Rolling, true);
		playerController.playerAnim.Roll();
		yield return new WaitForSeconds(rollTime);
		SetPlayerState(PlayerActions.Rolling, false);
	}
	
	private void OnDestroy()
	{
		SetPlayerState -= playerController.OnSetPlayerState;
	}
}
