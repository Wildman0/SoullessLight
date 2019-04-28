using System.Collections;
using System.Collections.Generic;
using NDA.PlayerInput;
using UnityEditor.Experimental.Rendering;
using UnityEngine;

/// <summary>
/// Manages various aspects of the game state
/// </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public InputController.ControllerType controller;
	public static InputController inputController = new XboxOneInputController();

    void Awake()
    {
        if (!instance)
            instance = this;
        //else
            //Debug.Log("More than one instance of GameManager in the scene");
        
        switch (controller)
        {
            case InputController.ControllerType.XboxOne:
                inputController = new XboxOneInputController();
                break;

            case InputController.ControllerType.Keyboard:
                inputController = new KeyboardInputController();
                break;

            default:
                //Debug.LogError("Invalid control scheme given");
                break;
        }
    }
}
