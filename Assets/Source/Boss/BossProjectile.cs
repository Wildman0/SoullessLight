using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dreamteck.Splines;

public class BossProjectile : MonoBehaviour
{
    public GameObject spawner;
    [SerializeField] private float projectileDamage = 0.1f;
    public static SplineComputer sc;
    private SplineFollower follower;
    [SerializeField] private Animator anim;

    //this will take place at the start of the game even when not loaded in. finds the spline computer
    private void Start()
    {
       sc = spawner.GetComponent<SplineComputer>();
       follower = GetComponent<SplineFollower>();
       follower.computer = sc;

    }

    // considered as OnStart(), ensures the spawn animation will play correctly
    private void OnEnable()
    {
        transform.localScale = new Vector3(0,0,0);
        anim.SetTrigger("StartSpawn");
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
        //follower.Restart(0);
        ObjectPoolManager.instance.RecallObject(gameObject);
    }

    //makes sure nothing is running when disabled
    private void OnDisable()
    {
        CancelInvoke();
    }
}
