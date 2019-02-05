using System.Collections;
using System.Collections.Generic;
using RootMotion.Demos;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PhaseValues))]
public class PhaseInspector : Editor
{
    public override void OnInspectorGUI()
    {
        PhaseName();

        EditorGUILayout.Space();

        PhaseActivation();

        EditorGUILayout.Space();

        CoolDownTimer();

        EditorGUILayout.Space();

        ComboChance();

        EditorGUILayout.Space();

        CloseAttacks();

        EditorGUILayout.Space();

        MidAttacks();

        EditorGUILayout.Space();

        LongAttacks();
    }

    private void PhaseName()
    {
        var phaseValues = (PhaseValues)target;

        EditorGUILayout.BeginHorizontal();

        EditorGUILayout.LabelField("Phase Name:", GUILayout.Width(130), GUILayout.Height(16));
        phaseValues.phaseName = EditorGUILayout.TextField(phaseValues.phaseName, GUILayout.Width(150), GUILayout.Height(16));

        EditorGUILayout.EndHorizontal();
    }

    private void PhaseActivation()
    {
        var phaseValues = (PhaseValues)target;

        EditorGUILayout.BeginHorizontal();

        EditorGUILayout.LabelField("Phase Activation: Max", GUILayout.Width(130), GUILayout.Height(16));
        phaseValues.min = EditorGUILayout.FloatField(phaseValues.min, GUILayout.Width(58), GUILayout.Height(16));
        EditorGUILayout.LabelField("Min", GUILayout.Width(25), GUILayout.Height(16));
        phaseValues.max = EditorGUILayout.FloatField(phaseValues.max, GUILayout.Width(58), GUILayout.Height(16));

        EditorGUILayout.EndHorizontal();
    }

    private void CoolDownTimer()
    {
        var phaseValues = (PhaseValues)target;

        EditorGUILayout.BeginHorizontal();

        EditorGUILayout.LabelField("Cool Down Timer", GUILayout.Width(130), GUILayout.Height(16));
        phaseValues.coolDownTimer = EditorGUILayout.FloatField(phaseValues.coolDownTimer, GUILayout.Width(58), GUILayout.Height(16));

        EditorGUILayout.EndHorizontal();
    }

    private void ComboChance()
    {
        var phaseValues = (PhaseValues)target;

        EditorGUILayout.BeginHorizontal();

        EditorGUILayout.LabelField("Combo Chance", GUILayout.Width(130), GUILayout.Height(16));
        phaseValues.comboChance = EditorGUILayout.FloatField(phaseValues.comboChance, GUILayout.Width(58), GUILayout.Height(16));

        EditorGUILayout.EndHorizontal();
    }

    private void CloseAttacks()
    {
        var phaseValues = (PhaseValues)target;

        //SerializedObject closeAttacks = new SerializedObject(phaseValues);
        //SerializedProperty closeProperty = closeAttacks.FindProperty("closeAttacks");
        //EditorGUILayout.PropertyField(closeProperty, true, GUILayout.Width(200));
        //closeAttacks.ApplyModifiedProperties();

        //SerializedObject closeDamage = new SerializedObject(phaseValues);
        //SerializedProperty closeDamageProperty = closeDamage.FindProperty("closeDamage");
        //EditorGUILayout.PropertyField(closeDamageProperty, true, GUILayout.Width(200));
        //closeDamage.ApplyModifiedProperties();

        EditorGUILayout.PropertyField(serializedObject.FindProperty("closeAttacks"), true);

        serializedObject.ApplyModifiedProperties();

    }

    private void MidAttacks()
    {
        var phaseValues = (PhaseValues)target;

        SerializedObject midAttacks = new SerializedObject(phaseValues);
        SerializedProperty midProperty = midAttacks.FindProperty("midAttacks");
        EditorGUILayout.PropertyField(midProperty, true, GUILayout.Width(250));

        midAttacks.ApplyModifiedProperties();
    }

    private void LongAttacks()
    {
        var phaseValues = (PhaseValues)target;

        SerializedObject longAttacks = new SerializedObject(phaseValues);
        SerializedProperty longProperty = longAttacks.FindProperty("longAttacks");
        EditorGUILayout.PropertyField(longProperty, true, GUILayout.Width(250));
        longAttacks.ApplyModifiedProperties();
    }
}
