using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRotation : MonoBehaviour
{
    public float rotationDelay;
    private float delay;
    public float smoothRotation;
    public float smoothFinish;
    [SerializeField]
    private float smoothRot;

    private bool rotateRight;
    private bool rotateLeft;
    //[HideInInspector]
    public bool noTargets;
    private bool stopRotation;

    private Quaternion bossRotation;

    private Phase phase;

    private void Start()
    {
        bossRotation = transform.rotation;

        delay = rotationDelay;

        phase = GetComponentInChildren<Phase>();
    }

    private void Update()
    {
        NoVisibleTargets();
        Rotate();
        IsIdle();
    }

    private void NoVisibleTargets()
    {
        if(noTargets == true && stopRotation == false)
        {
            RotatingDirection();
        }
    }

    private void RotatingDirection()
    {
        delay -= 1f * Time.deltaTime;
        if (delay <= 0)
        {
            if (PlayerDirection.direction == "Right")
            {
                rotateRight = true;
                delay = rotationDelay;
            }
            else if (PlayerDirection.direction == "Left")
            {
                rotateLeft = true;
                delay = rotationDelay;
            }
        }

        SmoothRotation();
    }

    private void Rotate()
    {
        if(rotateRight == true)
        {
            bossRotation *= Quaternion.AngleAxis(15, Vector3.up);

            rotateRight = false;
        }
        else if(rotateLeft == true)
        {
            bossRotation *= Quaternion.AngleAxis(-15, Vector3.up);

            rotateLeft = false;
        }
        transform.rotation = Quaternion.Lerp(transform.rotation, bossRotation, smoothRot * Time.deltaTime);
    }

    private void StopRotation()
    {
        stopRotation = true;
    }

    private void SmoothRotation()
    {
        smoothRot = noTargets == true ? smoothRot = smoothRotation :
                    noTargets == false ? smoothRot = smoothFinish : 0;
    }

    private void IsIdle()
    {
        if (phase.anim != phase.anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            stopRotation = true;
        }
        else
        {
            stopRotation = false;
        }
    }
}

