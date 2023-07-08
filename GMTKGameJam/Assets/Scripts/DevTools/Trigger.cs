using System;
using DragoRyu.Utilities;

namespace DragoRyu.DevTools
{
    public class Trigger 
    {
        private readonly Action _action;
        private readonly Action _resetAction;
        public bool Fire { get; private set; }
        public Trigger(Action action, Action resetAction = null)
        {
            this._action = action;
            this._resetAction = resetAction;
        }
        public void SetTrigger()
        {
            if (Fire) return;
            Fire = true;
            _action.SafeInvoke();
        }
        public void ResetTrigger()
        {
            if (!Fire) return;
            Fire = false;
            _resetAction.SafeInvoke();
        }
    }
}
