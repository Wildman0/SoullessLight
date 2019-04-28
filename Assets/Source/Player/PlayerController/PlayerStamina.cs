using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class PlayerStamina : MonoBehaviour
{
	public static PlayerStamina instance;
	
	public float stamina = 1.0f;
	public bool isUsingStaminaAction;
	
	[SerializeField] float maxStamina = 1.0f;
	[SerializeField] float staminaReplenishPerSecond = 0.3f;
	[SerializeField] float runningStaminaReductionPerSecond = 0.3f;
    private float staminaWait = 0.5f;


	public float rollingStaminaReduction = 0.2f;
    public Animator staminaLow;

	private void Start()
	{
		if (!instance)
			instance = this;
		else
			Debug.LogError("More than one instance of PlayerStamina");
	}

	void Update()
	{
		if (!IsUsingStaminaActions() && !isUsingStaminaAction)
		{
            staminaWait += Time.deltaTime;
            
            if (staminaWait > 0.5f) //Mini pause after using stamina [isaac]
            {
                ReplenishStamina();
            }
		}
		else
		{
            staminaWait = 0;

            if (PlayerController.instance.GetPlayerState(PlayerActions.Sprinting))
			{
				ReduceStamina();
                
			}
		}

        if (stamina <= 0.1)
        {
            staminaLow.SetTrigger("IsStaminaLow");
        }
        else if (stamina >= 0.1)
        {
            staminaLow.SetBool("IsStaminaLow", false);
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
	}

	//Reduces stamina by a specified amount
	public void ReduceStamina(float f)
	{
		stamina -= f;
	}

	//Returns whether or not the player is using actions that require stamina
	bool IsUsingStaminaActions()
	{
		return PlayerController.instance.GetPlayerState(PlayerActions.Rolling) ||
		       PlayerController.instance.GetPlayerState(PlayerActions.Sprinting);
	}
}
