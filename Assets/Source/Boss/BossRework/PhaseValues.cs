using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Script/Boss", menuName = "New Phase", order = -1)]
public class PhaseValues : ScriptableObject
{
    public string phaseName;
    public float min; // phase Activation min
    public float max; //phase Activation max
    public float coolDownTimer;
    public float comboChance;
    public float[] damage;

    [Header("Close Attacks")]
    public AttackAnimations closeAttacks;
    [Header("Mid Attacks")]
    public AttackAnimations midAttacks;
    [Header("Long Attacks")]
    public AttackAnimations longAttacks;

    public AttackDamage closeDamage;
}
