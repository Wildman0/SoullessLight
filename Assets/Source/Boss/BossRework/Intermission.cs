using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intermission : MonoBehaviour
{
    Phase phase;

    public List<float> intermissionTime = new List<float>();
    public int listNumber;
    private int intermissionCounter = 0;
    public Animator approaching;

    public bool activated;
    public bool orbsDestroyed;
    // added by Pedrek
    public List<GameObject> intermissionAttacks = new List<GameObject>();
    public float delayTimeOrbs;
    //

    private void Start()
    {
        phase = GetComponent<Phase>();
        listNumber = -1;
    }

    public void PlayerAnimation()
    {
        if (activated == false)
        {
            phase.anim.SetTrigger("IntermissionIn");
            OrbSetUp.activateOrb = true;
            activated = true;
            //added by Pedrek
            if(intermissionCounter == 0)
            {
                StartCoroutine("AIOne");
            }
            else
            {
                StartCoroutine("AITwo");
            }
            //
        }
        IntermissionTime();
    }

    private void IntermissionTime()
    {
        intermissionTime[listNumber] -= 1f * Time.deltaTime;
        if(intermissionTime[listNumber] <= 0)
        {
            phase.anim.SetBool("IntermissionOut", true);
            phase.intermissionCheck = false;
            phase.retrievedPhase = false;
            approaching.SetBool("Approaching", false);
            OrbSetUp.endIntermission = true;
            PlayerHealth.instance.TakeDamage(0.3f);

            //added by Pedrek
            foreach(GameObject obj in intermissionAttacks)
            {
                obj.SetActive(false);
            }
            //           

        }
        else if(orbsDestroyed == true)
        {
            phase.anim.SetBool("IntermissionOut", true);
            phase.intermissionCheck = false;
            phase.retrievedPhase = false;;
            approaching.SetBool("Approaching", false);
            intermissionCounter++;
            // added by Pedrek
            foreach (GameObject obj in intermissionAttacks)
            {
                obj.SetActive(false);
            }
            //
        }
        else if(intermissionTime[listNumber] <= 25)
        {
            approaching.SetBool("Approaching",true);
            intermissionCounter++;
        }
        intermissionCounter++;
    }

    private IEnumerator AIOne()
    {
        yield return new WaitForSeconds(delayTimeOrbs);
        intermissionAttacks[0].SetActive(true);
        intermissionAttacks[Random.Range(2, intermissionAttacks.Count)].SetActive(true);
    }

    private IEnumerator AITwo()
    {
        yield return new WaitForSeconds(delayTimeOrbs);
        intermissionAttacks[1].SetActive(true);
        intermissionAttacks[Random.Range(2, intermissionAttacks.Count)].SetActive(true);
    }
}
