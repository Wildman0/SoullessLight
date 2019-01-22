using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Holds data related to things that happened last frame
public class LastFrameData : MonoBehaviour
{
    public static Vector3 position;

    //Finds the position of the player
    void LateUpdate()
    {
        position = transform.position;
        Debug.Log("Yeet");
    }
}
