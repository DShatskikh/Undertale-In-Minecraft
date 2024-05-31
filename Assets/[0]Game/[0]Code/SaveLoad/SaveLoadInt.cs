using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class SaveLoadInt : SaveLoadBase
    {
        [SerializeField]
        private SaveKeyInt _saveKey;
        
        [SerializeField] 
        private int _value;

        [SerializeField] 
        private UnityEvent[] _events;
        
        public void Save(int value)
        {
            PlayerPrefs.SetInt(_saveKey.name, value);
            _value = value;
        }

        public override void Load()
        {
            _value = PlayerPrefs.GetInt(_saveKey.name);
            _events[_value].Invoke();
        }

        public override void Reset()
        {
            Save(0);
        }
    }
}