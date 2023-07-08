using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
namespace DragoRyu.Utilities
{
    public static class FunctionalUtilities
    {
        public static void SafeInvoke(this Action action)
        {
            if (action == null || action.GetInvocationList().Length == 0) return;
            action();
        }

        public static void SafeInvoke<T>(this Action<T> action, T value)
        {
            if (action == null || action.GetInvocationList().Length == 0) return;
            action(value);
        }

        public static T GetRandom<T>(this List<T> list)
        {
            return list[Random.Range(0, list.Count)];
        }
    }
}
