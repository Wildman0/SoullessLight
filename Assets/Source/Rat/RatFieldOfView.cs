﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatFieldOfView : MonoBehaviour
{
    public float viewRadius;
    [Range(0, 360)]
    public float viewAngle;
    public float rotationDelay = 0.01f;
    private float delay;

    public LayerMask playerMask;
    [HideInInspector]
    public LayerMask obstacleMask;

    [HideInInspector]
    public List<Transform> visibleTargets = new List<Transform>();
    [HideInInspector]
    public List<Transform> noVisibleTargets = new List<Transform>();

    private void Start()
    {
        StartCoroutine("FindTargetsWithDelay", .2f);
        delay = rotationDelay;
    }

    private IEnumerator FindTargetsWithDelay(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            FindVisibleTargets();
        }
    }

    private void FindVisibleTargets()
    {
        visibleTargets.Clear();
        noVisibleTargets.Clear();

        var targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, playerMask);

        for (int i = 0; i < targetsInViewRadius.Length; i++)
        {
            var target = targetsInViewRadius[i].transform;
            var dirToTarget = (target.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2)
            {
                float disToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, dirToTarget, disToTarget, obstacleMask))
                {
                    visibleTargets.Add(target);
                }
            }

            if (Vector3.Angle(transform.forward, dirToTarget) > viewAngle / 2)
            {
                var disToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, dirToTarget, disToTarget, obstacleMask))
                {
                    noVisibleTargets.Add(target);
                }
            }
        }

        if (noVisibleTargets.Count == 1)
        {
            NoTargetsVisible();
        }
    }

    public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }

    public void NoTargetsVisible()
    {
        delay -= 1f * Time.deltaTime;
        if (delay <= 0)
        {
            //if (PlayerDirection.direction == "Right")
            //{
            //    BossRotation.rightRotation = true;
            //    delay = rotationDelay;
            //}
            //else if (PlayerDirection.direction == "Left")
            //{
            //    BossRotation.leftRotation = true;
            //    delay = rotationDelay;
            //}
        }
    }
}