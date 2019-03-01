using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle : MonoBehaviour
{
    private AnimHandler animHandler;

    private void Start()
    {
        animHandler = GetComponent<AnimHandler>();
    }

    public void Pause()
    {
        animHandler.anim.SetBool("isIdle", true);
    }
}
