using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbSetUp : MonoBehaviour
{
    public Phase phase;

    [Range(0, 1)]
    public float health;
    public GameObject orbObject;

    public static bool spawnOrb;

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
            Debug.Log(orbPrefab);

            spawnOrb = false;
        }
    }

    private void OrbHealth()
    {
        if (health <= 0.001f)
        {
            spawnOrb = true;
            health = 1f;
        }
    }
}
