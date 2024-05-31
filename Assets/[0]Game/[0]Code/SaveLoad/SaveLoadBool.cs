using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class SaveLoadBool : SaveLoadBase
    {
        [SerializeField]
        private SaveKeyBool _saveKey;
        
        [SerializeField]
        private UnityEvent _trueEvent;

        [SerializeField]
        private UnityEvent _falseEvent;
        
        [SerializeField]
        private bool _isValue;

        public void Save(bool isValue)
        {
            PlayerPrefs.SetInt(_saveKey.name, isValue ? 1 : 0);
            _isValue = isValue;
        }

        public override void Load()
        {
            _isValue = PlayerPrefs.GetInt(_saveKey.name) == 1;

            if (_isValue)
                _trueEvent.Invoke();
            else
                _falseEvent.Invoke();
        }

        public override void Reset()
        {
            Save(false);
            Load();
        }
    }
}