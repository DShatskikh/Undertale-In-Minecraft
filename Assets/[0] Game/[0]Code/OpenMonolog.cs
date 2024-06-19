using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Localization.Tables;

namespace Game
{
    public class OpenMonolog : MonoBehaviour
    {
        [SerializeField, TextArea] 
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
        
        public void Open()
        {
            GameData.Monolog.Show(_texts);
            EventBus.OnCloseMonolog += _endEvent.Invoke;
        }
    }
}