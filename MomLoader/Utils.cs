// Log.cs
// By: mims
// On: 4/9/2020

using UnityEngine;

namespace MomLoader
{
    public static class Utils
    {
        public static void Log(string LogMessage, string ErrorLevel = "Alert")
        {
            Debug.Log($"MomLog<{ErrorLevel}>:{LogMessage}");
        }
    }
}