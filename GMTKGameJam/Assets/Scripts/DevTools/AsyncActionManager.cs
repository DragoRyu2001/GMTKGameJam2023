using System;
using System.Collections.Generic;
using DragoRyu.Utilities;

namespace DragoRyu.DevTools
{
    public class AsyncActionManager
    {
        private readonly Dictionary<string, Action> _asyncDictionary;
        private readonly RandomStringGenerator _randomStringGenerator;
        
        public AsyncActionManager()
        {
            _asyncDictionary = new Dictionary<string, Action>();
            _randomStringGenerator = new RandomStringGenerator().IncludeNumerics().IncludeCapitalLetters().SetFixedLength(10);
        }
        public string GenerateAction(Action action)
        {
            var key = _randomStringGenerator.Generate();
            if (key == null) return string.Empty;
            while(_asyncDictionary.ContainsKey(key))
            {
                key = _randomStringGenerator.Generate();
            }
            _asyncDictionary.Add(key, action);
            return key;
        }
        public void TriggerAction(string key)
        {
            if (!_asyncDictionary.ContainsKey(key)) return;
            var tmp = _asyncDictionary[key];
            _asyncDictionary.Remove(key);
            tmp.SafeInvoke();
        }
        public void SafeRemoveAction(string key)
        {
            if (!_asyncDictionary.ContainsKey(key)) return;
            _asyncDictionary.Remove(key);
        }
    }
}