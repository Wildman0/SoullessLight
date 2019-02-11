using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intermission : MonoBehaviour
{
    Phase phase;

    public List<float> intermissionTime = new List<float>();
    public int listNumber;

    public bool activated;
    private bool orbsDestroyed;

    private void Start()
    {
        phase = GetComponent<Phase>();
        listNumber = -1;
    }

    public void PlayerAnimation()
    {
        if (activated == false)
        {
            phase.anim.SetBool("IntermissionIn", true);
            activated = true;

            OrbSetUp.spawnOrb = true;
        }
        else if(activated == true)
        {
            phase.anim.SetBool("IntermissionIn", false);
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

            PlayerHealth.instance.TakeDamage(0.3f);
        }
        else if(orbsDestroyed == true)
        {
            phase.anim.SetBool("IntermissionOut", true);
            phase.intermissionCheck = false;
            phase.retrievedPhase = false;

            orbsDestroyed = false;
        }
    }
}
