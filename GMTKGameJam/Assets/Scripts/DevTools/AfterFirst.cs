using System;
using DragoRyu.Utilities;

namespace DragoRyu.DevTools
{
    public class AfterFirst
    {
        private readonly Action _action;
        private readonly Action _resetAction;
        public bool IsFirstCall { get; private set; }
        public bool IsFirstReset { get; private set; }
        public AfterFirst(Action action, Action resetAction = null)
        {
            IsFirstCall = true;
            IsFirstReset = true;
            _action = action;
            _resetAction = resetAction;
        }
        public void Stimulate()
        {
            if (IsFirstCall)
            {
                IsFirstCall = false;
                IsFirstReset = true;
                return;
            }
            _action.SafeInvoke();
        }
        public void Inhibit()
        {
            if (!IsFirstReset) return;
            IsFirstReset = false;
            IsFirstCall = true;
            _resetAction.SafeInvoke();
        }
    }
}
