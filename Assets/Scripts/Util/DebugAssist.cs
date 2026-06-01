using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DebugAssist
{
    public static void Log(string message, Color c)
    {
        Debug.Log(GetMessage(message, c));
    }

    static string GetMessage(string message, Color c)
    {
        return string.Format("<color=#{0:X2}{1:X2}{2:X2}>{3}</color>", (byte)(c.r * 255f), (byte)(c.g * 255f), (byte)(c.b * 255f), message);
    }

    public static void Log(string message, Color c, GameObject associatedObj)
    {
        Debug.Log(GetMessage(message, c), associatedObj);
    }
}
