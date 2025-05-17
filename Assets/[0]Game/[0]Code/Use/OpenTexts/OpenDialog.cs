using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Localization;

namespace Game
{
    public class OpenDialog : MonoBehaviour
    {
        [SerializeField]
        private Replica[] _replicas;

        [SerializeField]
        private bool _isNotBackCharacter;
        
        [SerializeField] 
        private UnityEvent _endEvent;
        
        public void Use()
        {
            GameData.Dialog.Show(_replicas, _isNotBackCharacter);
            EventBus.OnCloseDialog += _endEvent.Invoke;
        }
    }
}