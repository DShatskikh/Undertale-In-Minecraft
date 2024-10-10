using System;
using System.Collections;
using UnityEngine.Localization;

namespace Game
{
    public class LoadTextCommand
    {
        private readonly LocalizedString _localizedString;
        
        private string _result;
        public string Result => _result;

        public LoadTextCommand(LocalizedString localizedString)
        {
            _localizedString = localizedString;
        }

        public IEnumerator Await()
        {
            var _yesTextOperation = _localizedString.GetLocalizedStringAsync();
            
            while (!_yesTextOperation.IsDone)
                yield return null;

            _result = _yesTextOperation.Result;
        }
    }
}