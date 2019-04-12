using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dreamteck.Splines;

public class BossProjectileVelocity : MonoBehaviour
{
    [SerializeField] private float projectileDamage = 0.1f;
    [SerializeField] private Animator anim;
    public float speed = 10f;
    public float destroyTime = 10f;

    // considered as OnStart(), ensures the spawn animation will play correctly. this also sets the speed for the orb.
    private void Start()
    {
        anim.SetTrigger("StartSpawn");

        Rigidbody rig = GetComponent<Rigidbody>();
        rig.velocity = transform.forward * speed;
        Invoke("Destroy",destroyTime);
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
        if (other.tag == "ProjectileStopper")
        {
            if (!PlayerHealth.instance.isInvincible)
            {
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
