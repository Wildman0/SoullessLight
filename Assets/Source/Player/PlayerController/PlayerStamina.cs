using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStamina : MonoBehaviour
{
	public static PlayerStamina instance;
	
	public float stamina = 1.0f;

	public const float maxStamina = 1.0f;
	public const float staminaReplenishPerSecond = 0.3f;
	public const float runningStaminaReductionPerSecond = 0.3f;
	private const float rollingStaminaReduction = 0.2f;

	private void Awake()
	{
		if (!instance)
			instance = this;
		else
			Debug.LogError("More than one instance of PlayerStamina");
	}

	void Update()
	{
		if (!IsUsingStaminaActions())
		{
			ReplenishStamina();
		}
		else
		{
			ReduceStamina();
		}
	}

	void ReplenishStamina()
	{
		stamina += staminaReplenishPerSecond * Time.deltaTime;

		if (stamina > maxStamina)
			stamina = maxStamina;
	}

	//Reduces stamina over time (for things like running)
	void ReduceStamina()
	{
		stamina -= runningStaminaReductionPerSecond * Time.deltaTime;
		Debug.Log(stamina);
	}

	//Reduces stamina by a specified amount
	void ReduceStamina(float f)
	{
		stamina -= f;
	}

	//Returns whether or not the player is using actions that require stamina
	bool IsUsingStaminaActions()
	{
		return PlayerController.instance.GetPlayerState(PlayerActions.Rolling) ||
		       PlayerController.instance.GetPlayerState(PlayerActions.Sprinting);
	}

	public float GetRollCost()
	{
		return rollingStaminaReduction;
	}
}
