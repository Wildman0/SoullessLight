using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRotation : MonoBehaviour
{
    public float rotationDelay;
    public float delay;
    public float smoothRotation;

    private bool rotateRight;
    private bool rotateLeft;
    //[HideInInspector]
    public bool noTargets;
    private bool stopRotation;

    private Quaternion bossRotation;

    private void Start()
    {
        bossRotation = transform.rotation;

        delay = rotationDelay;
    }

    private void Update()
    {
        NoVisibleTargets();
        Rotate();
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
    }

    private void Rotate()
    {
        if(rotateRight == true)
        {
            bossRotation *= Quaternion.AngleAxis(10, Vector3.up);

            rotateRight = false;
        }
        else if(rotateLeft == true)
        {
            bossRotation *= Quaternion.AngleAxis(-10, Vector3.up);

            rotateLeft = false;
        }
        transform.rotation = Quaternion.Lerp(transform.rotation, bossRotation, 10 * smoothRotation * Time.deltaTime);
    }

    private void StopRotation()
    {
        stopRotation = true;
    }
}

