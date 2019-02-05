using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AttackInfo
{
    //public AnimationClip anim;
    //public AnimationClip rightAnim;
    //public float damage;
    //public float rightDamage;

    public List<AnimationClip> leftAttacks = new List<AnimationClip>();
    public List<AnimationClip> rightAttacks = new List<AnimationClip>();
}
