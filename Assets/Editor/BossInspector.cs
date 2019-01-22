using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(BossPhases))]
public class BossInspector : Editor
{
    public override void OnInspectorGUI()
    {
        Phases();
        SelectedPhase();

        EditorGUILayout.Space();
    }

    public void Phases()
    {
        EditorGUILayout.BeginHorizontal();

        EditorGUILayout.LabelField("Phases", EditorStyles.boldLabel);
        BossPhases.phase = (BossPhases.Phases)EditorGUILayout.EnumPopup(BossPhases.phase);

        EditorGUILayout.EndHorizontal();
    }

    public void SelectedPhase()
    {
        if(BossPhases.phase == BossPhases.Phases.Intro)
        {
            Intro();
        }
        else if (BossPhases.phase == BossPhases.Phases.PhaseOne)
        {
            PhaseOne();
        }
        if (BossPhases.phase == BossPhases.Phases.PhaseTwoIntro)
        {
            PhaseTwoIntro();
        }
        if (BossPhases.phase == BossPhases.Phases.PhaseTwo)
        {
            PhaseTwo();
        }
        if (BossPhases.phase == BossPhases.Phases.PhaseThreeIntro)
        {
            PhaseThreeIntro();
        }
        if (BossPhases.phase == BossPhases.Phases.PhaseThree)
        {
            PhaseThree();
        }
    }

    public void Intro()
    {

    }

    public void PhaseOne()
    {
        EditorGUILayout.Space();

        BossPhases phaseOne = (BossPhases)target;

        SerializedObject phaseOneCloseAttacks = new SerializedObject(phaseOne);
        SerializedProperty closeProperty = phaseOneCloseAttacks.FindProperty("phaseOneCloseAttacks");
        EditorGUILayout.PropertyField(closeProperty, true);
        phaseOneCloseAttacks.ApplyModifiedProperties();

        SerializedObject phaseOneLongAttacks = new SerializedObject(phaseOne);
        SerializedProperty longProperty = phaseOneLongAttacks.FindProperty("phaseOneLongAttacks");
        EditorGUILayout.PropertyField(longProperty, true);
        phaseOneLongAttacks.ApplyModifiedProperties();

        EditorGUILayout.Space();

        phaseOne.phaseOneCoolDownTimer = EditorGUILayout.FloatField("CoolDownTimer", phaseOne.phaseOneCoolDownTimer);
    }

    public void PhaseTwoIntro()
    {
        EditorGUILayout.Space();

        BossPhases phaseTwoIntro = (BossPhases) target;

        phaseTwoIntro.phaseTwoIntroIntermission =EditorGUILayout.FloatField("Phase Intro Timer", phaseTwoIntro.phaseTwoIntroIntermission);
    }

    public void PhaseTwo()
    {
        EditorGUILayout.Space();

        BossPhases phaseTwo = (BossPhases)target;

        SerializedObject phaseTwoCloseAttacks = new SerializedObject(phaseTwo);
        SerializedProperty closeProperty = phaseTwoCloseAttacks.FindProperty("phaseTwoCloseAttacks");
        EditorGUILayout.PropertyField(closeProperty, true);
        phaseTwoCloseAttacks.ApplyModifiedProperties();

        SerializedObject phaseTwoLongAttacks = new SerializedObject(phaseTwo);
        SerializedProperty longProperty = phaseTwoLongAttacks.FindProperty("phaseTwoLongAttacks");
        EditorGUILayout.PropertyField(longProperty, true);
        phaseTwoLongAttacks.ApplyModifiedProperties();

        EditorGUILayout.Space();

        phaseTwo.phaseTwoCoolDownTimer = EditorGUILayout.FloatField("CoolDownTimer", phaseTwo.phaseTwoCoolDownTimer);
    }

    public void PhaseThreeIntro()
    {
        EditorGUILayout.Space();

        BossPhases phaseThreeIntro = (BossPhases)target;

        phaseThreeIntro.phaseThreeIntroIntermission = EditorGUILayout.FloatField("Phase Intro Timer", phaseThreeIntro.phaseThreeIntroIntermission);
    }

    public void PhaseThree()
    {
        EditorGUILayout.Space();

        BossPhases phaseThree = (BossPhases)target;

        SerializedObject phaseThreeCloseAttacks = new SerializedObject(phaseThree);
        SerializedProperty closeProperty = phaseThreeCloseAttacks.FindProperty("phaseThreeCloseAttacks");
        EditorGUILayout.PropertyField(closeProperty, true);
        phaseThreeCloseAttacks.ApplyModifiedProperties();

        SerializedObject phaseThreeLongAttacks = new SerializedObject(phaseThree);
        SerializedProperty longProperty = phaseThreeLongAttacks.FindProperty("phaseThreeLongAttacks");
        EditorGUILayout.PropertyField(longProperty, true);
        phaseThreeLongAttacks.ApplyModifiedProperties();

        EditorGUILayout.Space();

        phaseThree.phaseThreeCoolDownTimer = EditorGUILayout.FloatField("CoolDownTimer", phaseThree.phaseThreeCoolDownTimer);
    }
}

