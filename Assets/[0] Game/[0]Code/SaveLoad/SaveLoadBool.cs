using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class SaveLoadBool : SaveLoadBase
    {
        [SerializeField]
        private UnityEvent _trueEvent;

        [SerializeField]
        private bool _isValue;

        public void Save(bool isValue)
        {
            PlayerPrefs.SetInt(_key, isValue ? 1 : 0);
            _isValue = isValue;
            
            GameData.Saver.SavePlayerPosition();
        }

        public override void Load()
        {
            _isValue = PlayerPrefs.GetInt(_key) == 1;

            if (_isValue)
                _trueEvent.Invoke();
        }

        public override void Reset()
        {
            Save(false);
            Load();
        }
    }
}