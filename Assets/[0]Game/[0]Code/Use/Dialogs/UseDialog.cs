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

        [SerializeField]
        private AudioClip _sound;
        
        public override void Use()
        {
            GameData.Dialog.SetReplicas(_replicas, _sound);
            EventBus.CloseDialog += _endEvent.Invoke;
        }
    }
}