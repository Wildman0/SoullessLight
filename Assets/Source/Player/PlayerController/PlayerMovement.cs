using System.Collections;
using System.Collections.Generic;
using NDA.FloatUtil;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	public PlayerController playerController;

	private int movementDisablers = 0;
	private bool movementDisabled;
	
	private float maxMoveDistance = 3.0f;
	private const float gravity = 9.8f;
	private Vector3 movementTarget;

	public Vector3 directionVector;
	public float velocity;

	void Update()
	{
		SetMovement();
		Move();
		ApplyGravity();
	}

	public void DisableMovement(float seconds)
	{
		StartCoroutine(DisableMovementIEnum(seconds));
	}

	IEnumerator DisableMovementIEnum(float seconds)
	{
		movementDisabled = true;
		movementDisablers++;
		yield return new WaitForSeconds(seconds);
		movementDisablers--;

		if (movementDisablers < 1)
			movementDisabled = false;

	}
	
	void SetMovement()
	{
		movementTarget = new Vector3(playerController.inputController.right - playerController.inputController.left,
			0,
			playerController.inputController.forward - playerController.inputController.back);

		movementTarget = transform.TransformDirection(movementTarget);
		SetDirectionVector();
	}

	void Move()
	{
		playerController.characterController.Move(Vector3.MoveTowards(Vector3.zero, movementTarget, maxMoveDistance * Time.deltaTime));
		
		playerController.playerAnim.Jog();
	}

	//Sets the direction the player is moving towards
	void SetDirectionVector()
	{
		directionVector = movementTarget * 10000;
	}

	void ApplyGravity()
	{
		playerController.characterController.Move(new Vector3(0, -gravity * Time.deltaTime, 0));
	}
}
