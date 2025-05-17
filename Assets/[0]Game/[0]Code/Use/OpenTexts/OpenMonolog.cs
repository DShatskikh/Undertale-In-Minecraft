using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Localization;

namespace Game
{
    public class OpenMonolog : MonoBehaviour
    {
        [SerializeField]
        private LocalizedString[] _localizedString;

        [SerializeField]
        private bool _isNotBackCharacter;
        
        [SerializeField] 
        private UnityEvent _endEvent;
        
        public void Use()
        {
            if (_localizedString.Length == 0)
                throw new Exception("Нету реплик");
            
            GameData.Monolog.Show(_localizedString.GetStrings(), _isNotBackCharacter);
            EventBus.OnCloseMonolog += _endEvent.Invoke;
        }
    }
}