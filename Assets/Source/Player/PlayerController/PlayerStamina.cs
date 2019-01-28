using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStamina : MonoBehaviour
{
	public static PlayerStamina instance;
	
	public float stamina = 1.0f;

	public const float staminaReplenishPerSecond = 0.3f;
	public const float runningStaminaReductionPerSecond = 0.3f;
	public const float rollingStaminaReduction = 0.2f;

	private void Start()
	{
		if (!instance)
			instance = this;
		else
			Debug.LogError("More than one instance of PlayerStamina");
	}

	void Update()
	{
		if (!isUsingStaminaActions())
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
		
	}

	void ReduceStamina()
	{
		
	}

	//Returns whether or not the player is using actions that require stamina
	bool IsUsingStaminaActions()
	{
		return PlayerController.instance.GetPlayerState(PlayerActions.Rolling) ||
		       PlayerController.instance.GetPlayerState(PlayerActions.Sprinting);
	}
}
