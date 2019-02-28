﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(RatFieldOfView))]
public class RatFieldOfViewEditor : Editor
{
    private void OnSceneGUI()
    {
        RatFieldOfView fow = (RatFieldOfView)target;

        Handles.color = Color.white;
        Handles.DrawWireArc(fow.transform.position, Vector3.up, Vector3.forward, 360, fow.viewRadius);
        Vector3 viewAngleA = fow.DirFromAngle(-fow.viewAngle / 2, false);
        Vector3 viewAngleB = fow.DirFromAngle(fow.viewAngle / 2, false);

        Handles.DrawLine(fow.transform.position, fow.transform.position + viewAngleA * fow.viewRadius);
        Handles.DrawLine(fow.transform.position, fow.transform.position + viewAngleB * fow.viewRadius);

        Handles.color = Color.red;
        foreach (Transform visibleTargets in fow.visibleTargets)
        {
            Handles.DrawLine(fow.transform.position, visibleTargets.position);
        }

        Handles.color = Color.blue;
        foreach (Transform NovisibleTargets in fow.noVisibleTargets)
        {
            Handles.DrawLine(fow.transform.position, NovisibleTargets.position);
        }
    }
}