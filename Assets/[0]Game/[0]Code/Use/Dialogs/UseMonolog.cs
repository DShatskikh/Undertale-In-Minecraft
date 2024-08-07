using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Localization;

namespace Game
{
    public class UseMonolog : UseObject
    {
        [SerializeField] 
        private UnityEvent _endEvent;

        [SerializeField]
        private LocalizedString[] _localizedStrings;
        
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