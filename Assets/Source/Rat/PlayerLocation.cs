using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLocation : MonoBehaviour
{
        
    #region Singleton

    public static PlayerLocation instance;

    private void Awake()
    {
        instance = this;

    }

    #endregion

    public GameObject player;
}
