using System.Collections;
using System.Collections.Generic;
using NDA.FloatUtil;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	public PlayerController playerController;

	private float maxMoveDistance = 3.0f;
	private float gravity = 9.8f;
	private Vector3 movementTarget;

	public Vector3 directionVector;

	void Update()
	{
		SetMovement();
		Move();
		ApplyGravity();
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
