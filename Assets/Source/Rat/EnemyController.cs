using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [Range(0, 10)]
    public float lookRadius = 10f;
    public float attackDamage;
    public float stoppingDistance;
    public float approxStoppingDistance;

    public bool isAttacking;

    Transform target;
    NavMeshAgent agent;

    AnimController animController;
    Attack attack;
    HitReg hitReg;
    RatHealth ratHealth;

	void Start ()
    {
        agent = GetComponent<NavMeshAgent>();

        target = PlayerLocation.instance.player.transform;

        animController = GetComponentInChildren<AnimController>();
        attack = GetComponent<Attack>();
        hitReg = GetComponent<HitReg>();
        ratHealth = GetComponent<RatHealth>();

        agent.stoppingDistance = stoppingDistance;
        approxStoppingDistance = agent.stoppingDistance + 0.2f;
	}
	
	void Update ()
    {
        HealthCheck();
	}

    private void HealthCheck()
    {
        if(ratHealth.health >= 0)
        {
            LookRadius();
        }
    }

    private void LookRadius()
    {
        float distance = Vector3.Distance(target.position, transform.position);

        if (distance <= lookRadius)
        {
            agent.SetDestination(target.position);
            animController.IsWalking();

            if (distance < approxStoppingDistance)
            {
                FaceTarget();
                attack.CoolDownTimer();

                if (isAttacking == true)
                {
                    animController.IsAttacking();
                    hitReg.ToggleHitreg();
                    isAttacking = false;
                }
                else
                {
                    animController.IsIdle();
                }
            }
        }
        else
        {
            agent.SetDestination(transform.position);
            animController.IsIdle();
        }
    }

    private void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
