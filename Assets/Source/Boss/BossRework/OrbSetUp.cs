using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbSetUp : MonoBehaviour
{
    public Phase phase;

    [Range(0, 1)]
    public float health;

    public int amountOfOrbs;

    public GameObject orbObject;
    public GameObject inSceneOrb;

    public static bool spawnOrb;

    public int animationPath;

    private void Start()
    {
        orbObject = Resources.Load<GameObject>("projectile");
    }

    private void Update()
    {
        SpawnOrb();
    }

    private void SpawnOrb()
    {
        if (spawnOrb == true)
        {
            GameObject orbPrefab = (GameObject)Instantiate(Resources.Load("projectile_Test"), new Vector3(-61.82f, 35.8f, 4.77f), Quaternion.identity);
            inSceneOrb = GameObject.FindGameObjectWithTag("Orb");
            amountOfOrbs -= 1;

            spawnOrb = false;
        }
    }

    private void OrbHealth()
    {
        if (health <= 0.001f && amountOfOrbs > 0)
        {
            spawnOrb = true;
            health = 1f;
        }
    }
}
