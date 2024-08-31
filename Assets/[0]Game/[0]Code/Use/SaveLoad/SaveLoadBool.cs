using UnityEngine;
using UnityEngine.Events;
using YG;

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
            YandexGame.savesData.SetInt(_key, isValue ? 1 : 0);
            _isValue = isValue;
            
            GameData.Saver.SavePlayerPosition();
        }

        public override void Load()
        {
            _isValue = YandexGame.savesData.GetInt(_key) == 1;

            if (_isValue)
                _trueEvent.Invoke();
        }

        [ContextMenu("Сбросить")]
        public override void Reset()
        {
            Save(false);
            Load();
        }
    }
}