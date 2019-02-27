using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathManager : MonoBehaviour
{
    public bool isWond;
    public bool isChasing;

    void Start()
    {
        //Sets iswond to true so the AI walks round the level at the begining of the game
        isWond = true;
    }

    void Update()
    { 
        if(isWond == true)
        {
            isChasing = false;
        }
        else if (isChasing == true)
        {
            isWond = false;
        }
    }

}
