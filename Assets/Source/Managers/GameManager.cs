using System.Collections;
using System.Collections.Generic;
using NDA.PlayerInput;
using UnityEngine;

/// <summary>
/// Manages various aspects of the game state
/// </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;
    public InputController.ControllerType controller;
	public static InputController inputController = new XboxOneInputController();

    void Awake()
    {
        switch (controller)
        {
            case InputController.ControllerType.XboxOne:
                inputController = new XboxOneInputController();
                break;

            case InputController.ControllerType.Keyboard:
                inputController = new KeyboardInputController();
                break;

            default:
                Debug.LogError("Invalid control scheme given");
                break;
        }
    }
}
