using System.Collections;
using System.Collections.Generic;
using NDA.FloatUtil;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
	public static PlayerHealth instance;
	
	public delegate void SetPlayerStateHandler(PlayerActions index, bool b);
	public event SetPlayerStateHandler SetPlayerState;
	
    [SerializeField] private Animator NearDeath;
	
	public float health = 1.0f;

	private const float maxHealth = 1.0f;
    [SerializeField] private float healAmount = 0.3f;
    [SerializeField] private float healTime = 2.7f;
    [SerializeField] private float healCool = 3f;
	
	public int healCount = 3;
    public float increasedAmount;

    public bool isInvincible;
	private bool isHealing;

	//Creates a singleton instance
	void Awake()
	{
		if (!instance)
			instance = this;
		//else 
			//Debug.LogError("More than one instance of PlayerHealth");
	}

	void Start()
	{
		SetPlayerState += PlayerController.instance.OnSetPlayerState;
	}
	
	void FixedUpdate()
	{
		HealInputCheck();
		
		//Heartbeat Effect Activate
		if (health < 0.25f && !PlayerController.instance.audioSource.isPlaying && health > 0f)
		{
			PlayerController.instance.audioSource.Play();
            NearDeath.SetBool("NearDeath", true);
            NearDeath.SetBool("NearDeath", true);
            LowHealthVibration.instance.SetVibration(true);
		}
		else if (health <= 0)
		{
			PlayerController.instance.audioSource.Stop();
			PlayerController.instance.audioSource1.Stop();
			LowHealthVibration.instance.SetVibration(false);
		}
		//Cancels the heartbeat effect
		else if (health > 0.25f) 
		{
            NearDeath.SetBool("NearDeath", false);
            LowHealthVibration.instance.SetVibration(false);
        }

        if (PlayerController.instance.GetPlayerState(PlayerActions.Healing))
        {
            HealingUI();
        }
    }


	//Heals the player
	void Heal()
	{
		PlayerAnim.instance.Heal();
		PlayerMovement.instance.DisableMovement(healTime);
		
		UI.instance.Healing.GetComponent<Image>().enabled = true;
		UI.instance.Healing.GetComponent<Animator>().SetTrigger("IsDamaged");

        increasedAmount = health + healAmount;
        healCount -= 1;
		
		UI.instance.SetPlayerHealthChargeCount(healCount);

		if (health > maxHealth)
		{
			health = maxHealth;
		}

		StartCoroutine(DisableHealing(healTime));
	}

    void HealingUI()
    {

        if (increasedAmount > health)
        {
            health += healAmount * Time.fixedDeltaTime;
        }


        if (health > maxHealth)
        {
            health = maxHealth;
        }
    }

    //Disallows the player from healing for a given amount of time
    private IEnumerator DisableHealing(float time)
	{
		SetPlayerState(PlayerActions.Healing, true);
		isHealing = true;
		yield return new WaitForSeconds(time);
		isHealing = false;
		SetPlayerState(PlayerActions.Healing, false);
	}
	
	//Damages the player by a given amount
	public void TakeDamage(float f)
	{
		if (!isInvincible)
		{
			health -= f;
			Controller.Vibrate(0, 0.5f, 0.5f);
			
			if (health < 0.01f)
			{
				Death();
			}
			else
			{
				PlayerAnim.instance.Flinch();
				PlayerMovement.instance.DisableMovement(0.3f);
				CameraController.instance.ShakeCamera(1.5f, 0.8f);
			}
		}
	}
	
	//Checks whether or not the heal input is active
	void HealInputCheck()
	{
		if (FloatCasting.ToBool(PlayerController.instance.inputController.healDown) && CanHeal())
		{
			Heal();
			//Debug.Log("Heal");
		}
	}
 
	//Returns whether or not the player is able to heal
	bool CanHeal()
	{
		return (!PlayerAnim.instance.anim.GetBool("IsHealing") &&
		        !PlayerController.instance.GetPlayerState(PlayerActions.HeavyAttacking) &&
		        !PlayerController.instance.GetPlayerState(PlayerActions.Rolling) &&
		        !isHealing && healCount > 0);
	}

	//Activates animations and UI elements associated with the player's death
	void Death()
	{
		PlayerAnim.instance.Death();
		PlayerMovement.instance.DisableMovement(99999f);
		isInvincible = true;
		
		UI.instance.deathImage.GetComponent<Image>().enabled = true;
		UI.instance.deathImage.GetComponent<Animator>().SetTrigger("isEnd");
	}
}
