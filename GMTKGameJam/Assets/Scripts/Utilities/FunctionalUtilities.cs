using System;

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
    }
}
