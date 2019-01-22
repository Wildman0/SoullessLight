using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHitDetection : MonoBehaviour
{
    private GameObject player;
    public  List<GameObject> objectsInCollider;
    public string[] tagsBeingLookedFor = {"Boss"};

    private void Start()
    {
        player = transform.parent.gameObject;
        objectsInCollider = new List<GameObject>();
    }

    void OnTriggerEnter(Collider coll)
    {
        for (int i = 0; i < tagsBeingLookedFor.Length; i++)
        {
            if (coll.gameObject.tag == tagsBeingLookedFor[i])
            {
                objectsInCollider.Add(coll.gameObject);
            }
        }
    }

    void OnTriggerExit(Collider coll)
    {
        for (int i = 0; i < tagsBeingLookedFor.Length; i++)
        {
            if (coll.gameObject.tag == tagsBeingLookedFor[i])
            {
                objectsInCollider.Remove(coll.gameObject);
            }
        }
    }
}
