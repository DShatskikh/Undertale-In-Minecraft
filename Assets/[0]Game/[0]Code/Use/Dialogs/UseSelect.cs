using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Localization;

namespace Game
{
    public class UseSelect : UseObject
    {
        [SerializeField] 
        private UnityEvent _yesEvent;
        
        [SerializeField] 
        private UnityEvent _noEvent;

        [SerializeField]
        private LocalizedString _localizedString;

        public override void Use()
        {
            GameData.Select.Show(_localizedString, Yes, No);
        }

        private void Yes()
        {
            _yesEvent.Invoke();
        }
        
        private void No()
        {
            _noEvent.Invoke();
        }
    }
}