// Config.cs
// By: mims
// On: 4/10/2020

using System.Collections.Generic;

namespace MomLoader
{
    public static class Tweakables
    {
        private static Dictionary<string, object> database = new Dictionary<string, object>
        {
            {"PickPocketMin", 0.1f},
            {"PickPocketMax", 5.0f}
        };

        public static T GetTweakable<T>(string name)
        {
            if (database.ContainsKey(name))
            {
                return (T)database[name];
            }

            Utils.Log($"Returning default value for {name}", "Warning!");
            return default(T);
        }
    }

}
