using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [Range(0, 10)]
    public float lookRadius = 10f;
    public float attackDamage;

    public bool isAttacking;

    Transform target;
    NavMeshAgent agent;

    AnimController animController;
    Attack attack;
    HitReg hitReg;

	void Start ()
    {
        agent = GetComponent<NavMeshAgent>();

        target = PlayerLocation.instance.player.transform;

        animController = GetComponentInChildren<AnimController>();
        attack = GetComponent<Attack>();
        hitReg = GetComponent<HitReg>();
	}
	
	void Update ()
    {
        LookRadius();
	}

    private void LookRadius()
    {
        float distance = Vector3.Distance(target.position, transform.position);

        if (distance <= lookRadius)
        {
            agent.SetDestination(target.position);
            animController.IsWalking();

            if (distance <= agent.stoppingDistance)
            {
                FaceTarget();
                attack.CoolDownTimer();

                if(isAttacking == true)
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
