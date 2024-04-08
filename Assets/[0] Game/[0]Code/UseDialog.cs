using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class UseDialog : UseObject
    {
        [SerializeField]
        private Replica[] _replicas;
        
        [SerializeField] 
        private UnityEvent _endEvent;
        
        public override void Use()
        {
            GameData.Dialog.SetReplicas(_replicas);
            EventBus.OnCloseDialog += _endEvent.Invoke;
        }
    }
}