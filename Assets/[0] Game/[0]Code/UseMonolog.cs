using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Localization.Tables;
using UnityEngine.Serialization;

namespace Game
{
    public class UseMonolog : UseObject
    {
        [SerializeField, TextArea()]
        private string[] _texts;
        
        [SerializeField] 
        private UnityEvent _endEvent;

        [SerializeField]
        private StringTableEntry[] _tableEntries;
        
        public string[] GetTexts => _texts;
        public StringTableEntry[] SetTableEntries
        {
            set { _tableEntries = value; }
        }

        public override void Use()
        {
            GameData.Monolog.Show(_texts);
            EventBus.OnCloseMonolog += _endEvent.Invoke;
        }
    }
}