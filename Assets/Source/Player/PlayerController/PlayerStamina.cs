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
		instance = this;
	}

	void Update()
	{
		
	}

	void ReplenishStamina()
	{
		
	}

	void ReduceStamina()
	{
		
	}
}
