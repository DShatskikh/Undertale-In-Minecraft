using UnityEngine;
using UnityEngine.Events;
using YG;

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
            YandexGame.savesData.SetInt(_key, value);
            _value = value;
        }

        public override void Load()
        {
            _value = YandexGame.savesData.GetInt(_key);
            _events[_value].Invoke();
        }

        public override void Reset()
        {
            Save(0);
        }
    }
}