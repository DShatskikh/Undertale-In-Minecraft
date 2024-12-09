using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class UseAction : UseObject
    {
        [SerializeField]
        private UnityEvent _event;
        
        public override void Use()
        {
            _event.Invoke();
        }
    }
}