using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;

public class GameStatus
{
    public int versionRelease;
    public int versionMajor;
    public int versionMinor;
    public int versionPatch;

    public GameStatus()
    {
        
    }
    
    public GameStatus(int[] version)
    {
        versionRelease = version[0];
        versionMajor = version[1];
        versionMinor = version[2];
        versionPatch = version[3];
    }
    
    public string GetVersionNumber()
    {
        return versionRelease + "." + versionMajor + "." + versionMinor + "." + versionPatch;
    }
}
