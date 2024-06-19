using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Localization;
using UnityEngine.Localization.Tables;

namespace Game
{
    public class UseDialog : UseObject
    {
        [SerializeField]
        private Replica[] _replicas;
        
        [SerializeField] 
        private UnityEvent _endEvent;

        public Replica[] GetReplicas => _replicas;
        
        public override void Use()
        {
            GameData.Dialog.SetReplicas(_replicas);
            EventBus.OnCloseDialog += _endEvent.Invoke;
        }
    }
}