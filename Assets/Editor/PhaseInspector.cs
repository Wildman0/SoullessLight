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
        
        //Shows phase name field
        phaseValues.phaseName = EditorGUILayout.TextField("Phase Name", phaseValues.phaseName, GUILayout.Width(250), GUILayout.Height(16));
    }

    private void PhaseActivation()
    {
        var phaseValues = (PhaseValues)target;

        EditorGUILayout.BeginHorizontal();

        EditorGUILayout.LabelField("Phase Activation: Min", GUILayout.Width(130), GUILayout.Height(16));
        phaseValues.min = EditorGUILayout.FloatField(phaseValues.min, GUILayout.Width(75), GUILayout.Height(16));
        EditorGUILayout.LabelField("Max", GUILayout.Width(30), GUILayout.Height(16));
        phaseValues.max = EditorGUILayout.FloatField(phaseValues.max, GUILayout.Width(75), GUILayout.Height(16));

        EditorGUILayout.Space();

        if (GUILayout.Button("i", GUILayout.Width(15), GUILayout.Height(14)))
        {
            
        }

        EditorGUILayout.EndHorizontal();
    }

    private void CoolDownTimer()
    {
        var phaseValues = (PhaseValues)target;

        EditorGUILayout.BeginHorizontal();

        //Shows cool down timer float field
        phaseValues.coolDownTimer = EditorGUILayout.FloatField("Cool Down Timer", phaseValues.coolDownTimer, GUILayout.Width(200), GUILayout.Height(16));

        EditorGUILayout.Space();

        if (GUILayout.Button("i", GUILayout.Width(15), GUILayout.Height(14)))
        {

        }

        EditorGUILayout.EndHorizontal();
    }

    private void ComboChance()
    {
        var phaseValues = (PhaseValues)target;

        EditorGUILayout.BeginHorizontal();

        //Shows combo chance float field
        phaseValues.comboChance = EditorGUILayout.FloatField("Combo Chance", phaseValues.comboChance, GUILayout.Width(200), GUILayout.Height(16));

        EditorGUILayout.Space();

        if (GUILayout.Button("i", GUILayout.Width(15), GUILayout.Height(14)))
        {

        }

        EditorGUILayout.EndHorizontal();
    }

    private void CloseAttacks()
    {
        var phaseValues = (PhaseValues)target;

        SerializedObject closeAttacks = new SerializedObject(phaseValues);
        SerializedProperty closeProperty = closeAttacks.FindProperty("closeAttacks");
        EditorGUILayout.PropertyField(closeProperty, true);
        closeAttacks.ApplyModifiedProperties();
    }

    private void MidAttacks()
    {
        var phaseValues = (PhaseValues)target;

        SerializedObject midAttacks = new SerializedObject(phaseValues);
        SerializedProperty midProperty = midAttacks.FindProperty("midAttacks");
        EditorGUILayout.PropertyField(midProperty, true);
        midAttacks.ApplyModifiedProperties();
    }

    private void LongAttacks()
    {
        var phaseValues = (PhaseValues)target;

        SerializedObject longAttacks = new SerializedObject(phaseValues);
        SerializedProperty longProperty = longAttacks.FindProperty("longAttacks");
        EditorGUILayout.PropertyField(longProperty, true);
        longAttacks.ApplyModifiedProperties();
    }
}
