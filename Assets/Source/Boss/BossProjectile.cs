using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dreamteck.Splines;

public class BossProjectile : MonoBehaviour
{
    public float destroyAfter = 2;
    public GameObject spawner;
    [SerializeField] private float projectileDamage = 0.1f;
    public static SplineComputer sc;
    private SplineFollower follower;

    private void Start()
    {
        sc = spawner.GetComponent<SplineComputer>();
        follower = GetComponent<SplineFollower>();
        follower.computer = sc; 
    }

    private void OnEnable()
    {
        Invoke("Destroy", destroyAfter);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (!PlayerHealth.instance.isInvincible)
            {
                PlayerHealth.instance.TakeDamage(projectileDamage);
            }
        }
    }

    private void Destroy()
    {
        gameObject.SetActive(false);
        follower.Restart(0);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }
}
