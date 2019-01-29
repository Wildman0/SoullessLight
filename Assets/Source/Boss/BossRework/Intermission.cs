using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intermission : MonoBehaviour
{
    Phase phase;
    public List<float> intermissionTime = new List<float>();

    public bool activated;

    private void Start()
    {
        phase = GetComponent<Phase>();
    }

    public void PlayerAnimation()
    {
        if (activated == false)
        {
            phase.anim.SetBool("IntermissionIn", true);
            activated = true;
        }
        else if(activated == true)
        {
            phase.anim.SetBool("IntermissionIn", false);
        }

        IntermissionTime();
    }

    private void IntermissionTime()
    {
        intermissionTime[0] -= 1f * Time.deltaTime;
        if(intermissionTime[0] <= 0)
        {
            intermissionTime.Remove(intermissionTime[0]);
            phase.anim.SetBool("IntermissionOut", true);
            phase.intermissionCheck = false;
            phase.retrievedPhase = false;
        }
    }
}
