using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [Range(0, 10)]
    public float timer;
    private float countUpTimer;

    private AnimController animController;
    private EnemyController enemyController;

    private void Start()
    {
        animController = GetComponentInChildren<AnimController>();
        enemyController = GetComponent<EnemyController>();
    }

    public void CoolDownTimer()
    {
        countUpTimer += 1f * Time.deltaTime;
        if (countUpTimer >= timer)
        {
            enemyController.isAttacking = true;
            countUpTimer = 0f;
        }
    }
}
