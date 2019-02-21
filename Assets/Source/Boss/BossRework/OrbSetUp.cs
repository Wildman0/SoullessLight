using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbSetUp : MonoBehaviour
{
    public Phase phase;

    public static float[] orbHealth = new float[2] { 1, 1 };

    public int animationIndex;
    public static int healthIndex;

    public static bool spawnOrb;
    private bool firstOrb = true;

    public string orbName;

    public GameObject orbObject;
    private GameObject inSceneOrb;

    private Animator anim;

    private void Start()
    {
        orbObject = Resources.Load<GameObject>(orbName);
        phase = GetComponent<Phase>();
    }

    private void Update()
    {
        SpawnCheck();
        OrbHealth();
        EndIntermission();
    }

    private void SpawnCheck()
    {
        if(spawnOrb == true)
        {
            Debug.Log("In");
            StartCoroutine(SpawnDelay());
        }
    }

    private void SpawnOrb()
    {
        if (healthIndex != orbHealth.Length)
        {
            GameObject orbPrefab = (GameObject)Instantiate(Resources.Load(orbName), new Vector3(-61.82f, 35.8f, 4.77f), Quaternion.identity);

            anim = GameObject.FindGameObjectWithTag("Orb").GetComponent<Animator>();
            animationIndex = Random.Range(1, 4);
            anim.SetInteger("PathIndex", animationIndex);
        }
    }

    private void OrbHealth()
    {
        if(orbHealth[healthIndex] <= 0.001f)
        {
            inSceneOrb = GameObject.FindGameObjectWithTag("Orb");
            Destroy(inSceneOrb);
            healthIndex += 1;

            spawnOrb = true;
        }
    }

    private IEnumerator SpawnDelay()
    {
        spawnOrb = false;

        if (firstOrb == true)
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

    private void EndIntermission()
    {

    }
}
