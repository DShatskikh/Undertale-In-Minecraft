using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Localization;

namespace Game
{
    public class OpenSelect : MonoBehaviour
    {
        [SerializeField]
        private LocalizedString _localizedString;
        
        [SerializeField] 
        private UnityEvent _yesEvent;
        
        [SerializeField] 
        private UnityEvent _noEvent;
        
        public void Use()
        {
            GameData.Select.Show(_localizedString.GetLocalizedString(), Yes, No);
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