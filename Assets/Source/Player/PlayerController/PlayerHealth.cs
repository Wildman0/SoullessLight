using System.Collections;
using System.Collections.Generic;
using NDA.FloatUtil;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
	public static PlayerHealth instance;
	
    [SerializeField] private Animator NearDeath;
	
	public float health = 1.0f;

	private const float maxHealth = 1.0f;

	private float healAmount = 0.3f;
	private float healTime = 2.7f;
	private float healCool = 3f;
	
	public int healCount = 3;

	public bool isInvincible;

	void Awake()
	{
		if (!instance)
			instance = this;
		else 
			Debug.LogError("More than one instance of PlayerHealth");
	}
	
	void FixedUpdate()
	{
		HealInputCheck();
		
		if (health < 0.25f && !PlayerController.instance.audioSource.isPlaying && health > 0f) //Heartbeat Effect Activate
		{
			PlayerController.instance.audioSource.Play();
            NearDeath.SetBool("NearDeath", true);
            NearDeath.SetBool("NearDeath", true);
        }
		else if (health <= 0)
		{
			PlayerController.instance.audioSource.Stop();
			PlayerController.instance.audioSource1.Stop();
			//StartCoroutine(playerController.Retry());

		}
		else if (health > 0.25f) //Heartbeat Effect Cancel (Isaac)
		{
            NearDeath.SetBool("NearDeath", false);
        }
	}
	
	//Heals the player
	void Heal()
	{
		PlayerAnim.instance.Heal();
		PlayerMovement.instance.DisableMovement(healTime);
		
		UI.instance.Healing.GetComponent<Image>().enabled = true;
		UI.instance.Healing.GetComponent<Animator>().SetTrigger("IsDamaged");
		
		health += healAmount;
		healCount -= 1;

		if (health > maxHealth)
		{
			health = maxHealth;
		}
	}
	
	public void TakeDamage(float f)
	{
		if (!isInvincible)
		{
			health -= f;

			if (health < 0.01f)
			{
				Death();
			}
			else
			{
				PlayerAnim.instance.Flinch();
				PlayerMovement.instance.DisableMovement(0.3f);
				PlayerController.instance.mainCamera.GetComponent<RFX4_CameraShake>().PlayShake();
			}
		}
	}
	
	void HealInputCheck()
	{
		if (FloatCasting.ToBool(PlayerController.instance.inputController.healDown) && CanHeal())
		{
			Heal();
			Debug.Log("Heal");
		}
	}

	bool CanHeal()
	{
		return (!PlayerAnim.instance.anim.GetBool("IsHealing") &&
		        /*(!playerController.isRolling) &&*/ healCount > 0);
	}

	void Death()
	{
		PlayerAnim.instance.Death();
		PlayerMovement.instance.DisableMovement(99999f);
		isInvincible = true;
		
		UI.instance.deathImage.GetComponent<Image>().enabled = true;
		UI.instance.deathImage.GetComponent<Animator>().SetTrigger("isEnd");
	}
}
