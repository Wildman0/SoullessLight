using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dreamteck.Splines;
using UnityEngine.UI;

public class TutorialProjectile : MonoBehaviour
{
    public GameObject implosion;
    public GameObject enableMe;
    public GameObject doorBlocker;
    public GameObject spawn;
    public GameObject UIDisable;
    public Image healthUI;
    private bool implode;

    public static float health = 3;

    private void Start()
    {
        enableMe.SetActive(false);
        doorBlocker = GameObject.Find("DoorBlocker");
        StartCoroutine(SpawnComplete());
        implode = false;


    
    }

    private void Update()
    {
        if (health <= 0 && !implode)
        {
            Implode();
            doorBlocker.active = false;
            Destroy(gameObject, 0.1f);           
            
        }

        if (health <= 0.02f)
        {
            healthUI.fillAmount = health;
        }
    }

    void Implode()
    {
        implode = true;
        GameObject implo = Instantiate(implosion, transform.position, Quaternion.identity);
        implo.GetComponent<ParticleSystem>().Play();
    }

    IEnumerator SpawnComplete()
    {
        yield return new WaitForSeconds(1.5f);
        enableMe.SetActive(true);
        Destroy(spawn);
    }

}
