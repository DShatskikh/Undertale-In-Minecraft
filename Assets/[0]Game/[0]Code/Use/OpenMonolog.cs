using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Localization;
using UnityEngine.Localization.Tables;
using UnityEngine.Serialization;

namespace Game
{
    public class OpenMonolog : MonoBehaviour
    {
        [SerializeField, TextArea] 
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
        
        public void Open()
        {
            GameData.Monolog.Show(_localizedStrings);
            EventBus.OnCloseMonolog += _endEvent.Invoke;
        }
    }
}