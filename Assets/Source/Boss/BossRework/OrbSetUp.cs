using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbSetUp : MonoBehaviour
{
    private Intermission intermission;

    public static bool activateOrb;
    public bool firstOrb;
    private bool selectPath;
    public static bool endIntermission;

    public static float health ;

    public int animationIndex;
    public int orbAmount;
    private int amountCheck;
    private float bossHealth;

    public string orbName;

    public GameObject OrbUi;
    private GameObject orb;

    public static Animator anim;
    public AudioSource orbSpawn;
    public AudioSource orbDie;


    private void Start()
    {
        intermission = GetComponent<Intermission>();

        GameObject orbPrefab = (GameObject)Instantiate(Resources.Load(orbName), new Vector3(-61.82f, 35.8f, 4.77f), Quaternion.identity);
        orb = GameObject.FindGameObjectWithTag("Orb");
        orb.SetActive(false);
        OrbUi.SetActive(false);

        health = .5f;

        bossHealth = GetComponent<BossHealth>().health;
    }

    private void Update()
    {
        ActivationCheck();
        OrbHealth();
        EndIntermission();
    }

    //Checks if boss is in intermission stage
    private void ActivationCheck()
    {
        if(activateOrb == true)
        {
            StartCoroutine(ActivationDelay());
        }
    }

    //Makes orb visible in game and and chooses a random destination for the orb to go to
    private void ActivateOrb()
    {
        if (amountCheck != orbAmount)
        {
            if (selectPath == false && activateOrb == true)
            {
                orb.SetActive(true);
                OrbUi.SetActive(true);
                anim = orb.GetComponent<Animator>();
                animationIndex = Random.Range(1, 4);
                anim.SetInteger("PathIndex", animationIndex);
                orbSpawn.Play();

                selectPath = true;
            }
        }
    }

    //Checks the orbs health. if health hits 0 orb will reset to go spawn again
    private void OrbHealth()
    {
        if(health <= 0)
        {
            orb.SetActive(false);
            orbDie.Play();
            orb.transform.position = new Vector3(-61.82f, 35.8f, 4.77f);
            amountCheck += 1;
            selectPath = false;
            health = .5f;
        }
    }

    private IEnumerator ActivationDelay()
    {
        if(firstOrb == false && bossHealth < 1f)
        {
            yield return new WaitForSeconds(7);
            ActivateOrb();

            firstOrb = true;
        }
        else if (bossHealth < 1f)
        {
            yield return new WaitForSeconds(2);
            ActivateOrb();
        }
    }

    private void EndIntermission()
    {
        if (amountCheck == orbAmount || endIntermission == true)
        {
            orb.SetActive(false);
            OrbUi.SetActive(false);
            intermission.orbsDestroyed = true;
            activateOrb = false;

            StartCoroutine(ResetValues());
        }
    }

    private IEnumerator ResetValues()
    {
        yield return new WaitForSeconds(2f);
        health = .5f;
        amountCheck = 0;
        endIntermission = false;
        orb.transform.position = new Vector3(-61.82f, 35.8f, 4.77f);
        intermission.orbsDestroyed = false;
        selectPath = false;
        firstOrb = false;
    }
}