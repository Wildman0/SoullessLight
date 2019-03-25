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

    public static float health;

    public int animationIndex;
    public int orbAmount;
    public int amountCheck;

    public string orbName;

    public GameObject orb;

    public static Animator anim;


    private void Start()
    {
        intermission = GetComponent<Intermission>();

        GameObject orbPrefab = (GameObject)Instantiate(Resources.Load(orbName), new Vector3(-61.82f, 35.8f, 4.77f), Quaternion.identity);
        orb = GameObject.FindGameObjectWithTag("Orb");
        orb.SetActive(false);

        health = 1f;
    }

    private void Update()
    {
        ActivationCheck();
        orbHealth();
        EndIntermission();
    }

    private void ActivationCheck()
    {
        if(activateOrb == true)
        {
            StartCoroutine(ActivationDelay());
        }
    }

    private void ActivateOrb()
    {
        if (amountCheck != orbAmount)
        {
            if (selectPath == false)
            {
                orb.SetActive(true);
                anim = orb.GetComponent<Animator>();
                animationIndex = Random.Range(1, 4);
                anim.SetInteger("PathIndex", animationIndex);

                selectPath = true;
            }
        }
    }

    private void orbHealth()
    {
        if(health <= 0)
        {
            orb.SetActive(false);
            orb.transform.position = new Vector3(-61.82f, 35.8f, 4.77f);
            amountCheck += 1;
            selectPath = false;
            health = 1f;
        }
    }

    private IEnumerator ActivationDelay()
    {
        if(firstOrb == false)
        {
            yield return new WaitForSeconds(7);
            ActivateOrb();

            firstOrb = true;
        }
        else
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
            intermission.orbsDestroyed = true;
            activateOrb = false;

            StartCoroutine(ResetValues());
        }
    }

    private IEnumerator ResetValues()
    {
        yield return new WaitForSeconds(2f);
        health = 1f;
        amountCheck = 0;
        endIntermission = false;
        orb.transform.position = new Vector3(-61.82f, 35.8f, 4.77f);
        intermission.orbsDestroyed = false;
        //selectPath = false;
    }
}