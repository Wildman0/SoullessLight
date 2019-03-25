using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dreamteck.Splines;

public class BossProjectileVelocity : MonoBehaviour
{
    public GameObject spawner;
    [SerializeField] private float projectileDamage = 0.1f;
    public static SplineComputer sc;
    private SplineFollower follower;
    [SerializeField] private Animator anim;
    public float zSpeed = 10f;


    // considered as OnStart(), ensures the spawn animation will play correctly. this also sets the speed for the orb.
    private void OnEnable()
    {
        transform.localScale = new Vector3(0,0,0);
        anim.SetTrigger("StartSpawn");

        Vector3 force = new Vector3(0, 0, zSpeed);

        GetComponent<Rigidbody>().velocity = force;

    }

    // when colliding with player and they are not rolling run destroy()
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (!PlayerHealth.instance.isInvincible)
            {
                PlayerHealth.instance.TakeDamage(projectileDamage);
                Destroy();
            }
        }
    }

    // disable when reached end (done via triggers in the follower), and reset position
    public void Destroy()
    {
        ObjectPoolManager.instance.RecallObject(gameObject);
    }

    //makes sure nothing is running when disabled
    private void OnDisable()
    {
        CancelInvoke();
    }
    
}
