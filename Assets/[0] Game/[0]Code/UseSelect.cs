using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Localization.Tables;

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

        [SerializeField]
        private StringTableEntry _tableEntry;
        
        public string GetText => _text;

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

        public void SetTableEntry(StringTableEntry tableEntry)
        {
            _tableEntry = tableEntry;
        }
    }
}