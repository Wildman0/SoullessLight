using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbSetUp : MonoBehaviour
{
    private Intermission intermission;

    public static bool activateOrb;
    public bool firstOrb;

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
        if(amountCheck != orbAmount)
        {
            orb.SetActive(true);
            anim = orb.GetComponent<Animator>();
            animationIndex = Random.Range(1, 4);
            anim.SetInteger("PathIndex", animationIndex);
        }
    }

    private void orbHealth()
    {
        if(health <= 0)
        {
            orb.SetActive(false);
            orb.transform.position = new Vector3(-61.82f, 35.8f, 4.77f);
            health = 1f;

            amountCheck += 1;
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
        if(amountCheck == orbAmount)
        {
            intermission.orbsDestroyed = true;
            activateOrb = false;

            health = 1f;
            amountCheck = 0;

        }
    }
}

//{
//    public Phase phase;

//public static float[] orbHealth = new float[2] { 1, 1 };

//public int animationIndex;
//public static int healthIndex;

//public static bool spawnOrb;
//private bool firstOrb = true;

//public string orbName;

//public GameObject orbObject;
//private GameObject inSceneOrb;

//public static Animator anim;

//Intermission intermission;

//private void Start()
//{
//    intermission = GetComponent<Intermission>();

//    orbObject = Resources.Load<GameObject>(orbName);
//    phase = GetComponent<Phase>();
//}

//private void Update()
//{
//    SpawnCheck();
//    OrbHealth();
//    EndIntermission();
//}

//private void SpawnCheck()
//{
//    if (spawnOrb == true)
//    {
//        StartCoroutine(SpawnDelay());
//    }
//}

//private void SpawnOrb()
//{
//    if (healthIndex != orbHealth.Length)
//    {
//        GameObject orbPrefab = (GameObject)Instantiate(Resources.Load(orbName), new Vector3(-61.82f, 35.8f, 4.77f), Quaternion.identity);

//        anim = GameObject.FindGameObjectWithTag("Orb").GetComponent<Animator>();
//        animationIndex = Random.Range(1, 4);
//        anim.SetInteger("PathIndex", animationIndex);
//    }
//}

//private void OrbHealth()
//{
//    if (orbHealth[healthIndex] <= 0.001f)
//    {
//        inSceneOrb = GameObject.FindGameObjectWithTag("Orb");
//        Destroy(inSceneOrb);
//        healthIndex += 1;

//        spawnOrb = true;
//    }
//}

//private IEnumerator SpawnDelay()
//{
//    spawnOrb = false;

//    if (firstOrb == true)
//    {
//        yield return new WaitForSeconds(7);
//        firstOrb = false;

//        SpawnOrb();
//    }
//    else
//    {
//        yield return new WaitForSeconds(2);

//        SpawnOrb();
//    }
//}

//private void EndIntermission()
//{
//    if (healthIndex == orbHealth.Length)
//    {
//        intermission.orbsDestroyed = true;
//    }
//}
