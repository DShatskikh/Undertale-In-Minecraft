using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Localization;
using UnityEngine.Localization.Tables;
using UnityEngine.Serialization;

namespace Game
{
    public class UseMonolog : UseObject
    {
        [SerializeField, TextArea()]
        private string[] _texts;
        
        [SerializeField] 
        private UnityEvent _endEvent;

        [SerializeField]
        private LocalizedString[] _localizedStrings;
        
        public string[] GetTexts => _texts;
        public LocalizedString[] SetLocalizedStrings
        {
            set { _localizedStrings = value; }
        }

        public override void Use()
        {
            GameData.Monolog.Show(_localizedStrings);
            EventBus.OnCloseMonolog += _endEvent.Invoke;
        }
    }
}