using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAnim : MonoBehaviour
{
    public static Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }
}
