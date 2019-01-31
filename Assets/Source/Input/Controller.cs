using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

public class Controller : MonoBehaviour
{
    private static int queuedOrders;
    public static Controller instance;

    void Awake()
    {
        instance = this;
        Debug.Log(instance);
    }

    //Makes a given controller vibrate at a given intensity for a given amount of time
    public static void Vibrate(int index, float intensity, float time)
    {
        instance.StartCoroutine(instance.VibrateCoroutine(index, intensity, intensity, time));
    }

    //Makes a given controller vibrate at a given intensity for a given amount of time
    //Different from previous method as you are able to specify the amount that it should
    //Vibrate on the left/right
    public static void Vibrate(int index, float intensityLeft, float intensityRight, float time)
    {
        instance.StartCoroutine(instance.VibrateCoroutine(index, intensityLeft, intensityRight, time));
    }

    IEnumerator VibrateCoroutine(int index, float intensityLeft, float intensityRight, float time)
    {
        GamePad.SetVibration((PlayerIndex) index, intensityLeft, intensityRight);

        queuedOrders++;
        yield return new WaitForSeconds(time);
        queuedOrders--;

        if (queuedOrders == 0)
        {
            CancelVibration();
        }
    }

    private static void CancelVibration()
    {
        GamePad.SetVibration(0, 0, 0);
    }
}

