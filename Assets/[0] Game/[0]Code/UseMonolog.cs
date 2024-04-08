using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class UseMonolog : UseObject
    {
        [SerializeField, TextArea()]
        private string[] _texts;
        
        [SerializeField] 
        private UnityEvent _endEvent;
        
        public override void Use()
        {
            GameData.Monolog.Show(_texts);
            EventBus.OnCloseMonolog += _endEvent.Invoke;
        }
    }
}