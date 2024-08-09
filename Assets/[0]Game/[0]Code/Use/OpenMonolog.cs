using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Localization;

namespace Game
{
    public class OpenMonolog : MonoBehaviour
    {
        [SerializeField] 
        private UnityEvent _endEvent;
        
        [SerializeField]
        private LocalizedString[] _localizedStrings;

        public void Open()
        {
            GameData.Monolog.Show(_localizedStrings);
            EventBus.OnCloseMonolog += _endEvent.Invoke;
        }
    }
}