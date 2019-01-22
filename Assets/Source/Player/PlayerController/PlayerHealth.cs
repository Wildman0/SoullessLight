using System.Collections;
using System.Collections.Generic;
using NDA.FloatUtil;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
	[SerializeField] private PlayerController playerController;
	
	public float health = 1.0f;
	private const float maxHealth = 1.0f;

	private float healAmount = 0.2f;
	private float healTime = 2.2f;
	private float healCool = 3f;
	
	public int healCount = 3;

	void FixedUpdate()
	{
		HealInputCheck();
		
		if (health < 0.25f && !playerController.audioSource.isPlaying && health > 0f) //Heartbeat Effect Activate
		{
			playerController.audioSource.Play();
			playerController.NearDeath.GetComponent<Animator>().SetBool("NearDeath", true);
		}
		else if (health <= 0)
		{
			playerController.audioSource.Stop();
			playerController.audioSource1.Stop();
			StartCoroutine(playerController.Retry());

		}
		else if (health > 0.25f) //Heartbeat Effect Cancel (Isaac)
		{
			playerController.NearDeath.GetComponent<Animator>().SetBool("NearDeath", false);
		}
	}
	
	//Heals the player
	void Heal()
	{
		playerController.playerAnim.Heal();
		playerController.DisableMovement(healTime);
		
		playerController.ui.Healing.GetComponent<Image>().enabled = true;
		playerController.ui.Healing.GetComponent<Animator>().SetTrigger("IsDamaged");
		
		health += healAmount;
		healCount -= 1;

		if (health > maxHealth)
		{
			health = maxHealth;
		}
	}
	
	public void TakeDamage(float f)
	{
		health = f;

		if (health < 0.01f)
		{
			playerController.Death();
		}
		else
		{
			playerController.playerAnim.Flinch();
			playerController.DisableMovement(0.3f);
			playerController.mainCamera.GetComponent<RFX4_CameraShake>().PlayShake();
		}
	}
	
	void HealInputCheck()
	{
		if (FloatCasting.ToBool(playerController.inputController.healDown) && CanHeal())
		{
			Heal();
			Debug.Log("Heal");
		}
	}

	bool CanHeal()
	{
		return (!playerController.playerAnim.anim.GetBool("IsHealing") &&
		        (!playerController.isRolling) && healCount > 0);
	}
}
