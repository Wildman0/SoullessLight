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
    [SerializeField] private Animator anim;

    private void Start()
    {
       sc = spawner.GetComponent<SplineComputer>();
       follower = GetComponent<SplineFollower>();
       follower.computer = sc;
    }

    private void OnEnable()
    {
        transform.localScale = new Vector3(0,0,0);
        Invoke("Destroy", destroyAfter);
        anim.SetTrigger("StartSpawn");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Destroy();

            if (!PlayerHealth.instance.isInvincible)
            {
                PlayerHealth.instance.TakeDamage(projectileDamage);
            }
        }
    }

    public void Destroy()
    {
        gameObject.SetActive(false);
        follower.Restart(0);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }
}
