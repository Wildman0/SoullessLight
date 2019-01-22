using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class HelpMenu : EditorWindow
{
    bool fold = true;
    Vector4 rotationComponents;
    Transform selectedTransform;


    public static void ShowWindow()
    {
        HelpMenu menu = (HelpMenu) EditorWindow.GetWindow(typeof(HelpMenu));

        menu.Show();
    }

    void OnGUI()
    {

    }
}

