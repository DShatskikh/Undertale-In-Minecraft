using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class SaveLoadInt : SaveLoadBase
    {
        [SerializeField] 
        private int _value;

        [SerializeField] 
        private UnityEvent[] _events;
        
        public void Save(int value)
        {
            PlayerPrefs.SetInt(_key, value);
            _value = value;
            
            GameData.Saver.SavePlayerPosition();
        }

        public override void Load()
        {
            _value = PlayerPrefs.GetInt(_key);
            _events[_value].Invoke();
        }

        public override void Reset()
        {
            Save(0);
        }
    }
}