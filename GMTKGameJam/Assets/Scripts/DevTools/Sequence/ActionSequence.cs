using System;
using DragoRyu.Utilities;

namespace DragoRyu.DevTools.Sequence
{
    public sealed class ActionSequence: ISequence
    {
        private readonly Action _action;
        private readonly Trigger _trigger;
        private readonly bool _isTrigger;
        
        public ActionSequence(Action action, bool isTrigger = false)
        {
            _isTrigger = isTrigger;
            
            if (isTrigger) _trigger = new Trigger(action);
            else _action = action;
        }

        public void Trigger()
        {
            if(_isTrigger)_trigger.SetTrigger();
            else _action.SafeInvoke();
        }

        public void Reset()
        {
            if(_isTrigger)_trigger.ResetTrigger();
            else _action.SafeInvoke();
        }
    }
}