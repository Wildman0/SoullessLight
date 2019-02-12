using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbSetUp : MonoBehaviour
{
    public Phase phase;

    public static float orbHealth;
    public float showHealth;

    public int amountOfOrbs;

    public GameObject orbObject;
    public GameObject inSceneOrb;

    public Animator orbAnim;

    public static bool spawnOrb;
    private bool firstOrb;

    private int orbIndex;

    private void Start()
    {
        orbObject = Resources.Load<GameObject>("projectile");
        phase = GetComponent<Phase>();
        firstOrb = true;
        orbHealth = 1f;
    }

    private void Update()
    {
        SpawnCheck();
        OrbHealth();
        EndIntermission();

        showHealth = orbHealth;

        Debug.Log(spawnOrb);
    }

    private void SpawnCheck()
    {
        if(spawnOrb == true)
        {
            StartCoroutine(SpawnDelay());
        }
    }

    private IEnumerator SpawnDelay()
    {
        spawnOrb = false;

        if(firstOrb == true)
        {
            yield return new WaitForSeconds(7);
            firstOrb = false;

            SpawnOrb();
        }
        else
        {
            yield return new WaitForSeconds(2);

            SpawnOrb();
        }
    }

    private void SpawnOrb()
    {
        GameObject orbPrefab = (GameObject)Instantiate(Resources.Load("projectile_Test"), new Vector3(-61.82f, 35.8f, 4.77f), Quaternion.identity);
        amountOfOrbs -= 1;

        orbAnim = GameObject.FindGameObjectWithTag("Orb").GetComponent<Animator>();
        orbIndex = Random.Range(1, 4);
        orbAnim.SetInteger("PathIndex", orbIndex);
    }

    private void OrbHealth()
    {
        if(orbHealth <= 0.001f && amountOfOrbs > 0)
        {
            orbHealth = 1f;
            inSceneOrb = GameObject.FindGameObjectWithTag("Orb");
            Destroy(inSceneOrb);

            spawnOrb = true;
        }
    }

    private void EndIntermission()
    {
        if(amountOfOrbs <= 0)
        {
            //End Intermission phase
            orbHealth = 1f;

        }
    }
}
