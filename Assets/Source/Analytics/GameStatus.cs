using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;

public class GameStatus : MonoBehaviour
{
    public int versionRelease;
    public int versionMajor;
    public int versionMinor;
    public int versionPatch;
    
    public string GetVersionNumber()
    {
        return versionRelease + "." + versionMajor + "." + versionMinor + "." + versionPatch;
    }
}
