using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class UseSelect : UseObject
    {
        [SerializeField, TextArea]
        private string _text;

        [SerializeField] 
        private UnityEvent _yesEvent;
        
        [SerializeField] 
        private UnityEvent _noEvent;
        
        public override void Use()
        {
            GameData.Select.Show(_text, Yes, No);
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