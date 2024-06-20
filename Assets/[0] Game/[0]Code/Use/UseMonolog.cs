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
            var texts = new string[_texts.Length];

            for (int i = 0; i < texts.Length; i++) 
                texts[i] = _localizedStrings[i].GetLocalizedString();

            GameData.Monolog.Show(texts);
            EventBus.OnCloseMonolog += _endEvent.Invoke;
        }
    }
}