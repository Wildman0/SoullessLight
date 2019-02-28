using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimHandler : MonoBehaviour
{
    public static Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }
}
