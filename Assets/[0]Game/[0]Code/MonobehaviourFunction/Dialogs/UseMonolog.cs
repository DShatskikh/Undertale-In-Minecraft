using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Localization;

namespace Game
{
    public class UseMonolog : UseObject
    {
        [SerializeField]
        private LocalizedString[] _localizedStrings;

        [SerializeField] 
        private UnityEvent _endEvent;

        [SerializeField]
        private AudioClip _sound;

        public override void Use()
        {
            GameData.Monolog.Show(_localizedStrings, _sound);
            EventBus.CloseMonolog += _endEvent.Invoke;
        }
    }
}