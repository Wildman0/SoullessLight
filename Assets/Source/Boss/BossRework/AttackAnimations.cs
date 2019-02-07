using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct AnimationAttackData
{
    public AnimationClip animationClip;
    public float attackDamage;
}


[Serializable]
public class AttackAnimations
{
    public List<AnimationAttackData> leftAttacks = new List<AnimationAttackData>();
    public List<AnimationAttackData> rightAttacks = new List<AnimationAttackData>();
}
