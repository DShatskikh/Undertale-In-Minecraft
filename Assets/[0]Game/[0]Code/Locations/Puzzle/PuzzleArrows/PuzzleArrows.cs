using System;
using System.Collections.Generic;
using PixelCrushers;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Localization;
using YG;

namespace Game
{
    public class PuzzleArrows : Saver, IUseObject
    {
        [SerializeField]
        private PuzzleArrowView _view;

        [SerializeField]
        private PuzzleArrowSlotData[] _slotsData;

        [SerializeField]
        private LocalizedString _localizedString;

        [SerializeField]
        private UnityEvent _event;
        
        public PuzzleArrowSlotData[] SlotsData => _slotsData;

        private Data _saveData = new();
        
        [Serializable]
        public class Data
        {
            public bool IsDecision;
        }
        
        public override string RecordData()
        {
            return SaveSystem.Serialize(_saveData);
        }

        public override void ApplyData(string s)
        {
            var data = SaveSystem.Deserialize(s, _saveData);
            _saveData = data;

            foreach (var slotData in _slotsData)
            {
                slotData.ArrowDirection.Changed += (direction) => OnArrowChanged(direction, slotData);
                slotData.ArrowDirection.Value = _saveData.IsDecision ? slotData.Decision : slotData.StartArrowDirection;
                slotData.ArrowSpriteRenderer.sprite = slotData.View;
            }
            
            if (_saveData.IsDecision)
                _event.Invoke();
        }
        
        private void OnArrowChanged(ArrowDirection direction, PuzzleArrowSlotData data)
        {
            data.ArrowCenter.eulerAngles = direction.GetAngle() - data.StartArrowDirection.GetAngle();
        }
        
        public void Use()
        {
            GameData.CharacterController.enabled = false;

            if (!_saveData.IsDecision)
            {
                _view.SetModel(this);
                _view.Activate(true);
            }
            else
            {
                GameData.Monolog.Show(new []{_localizedString});
            }
        }

        public bool TryDecision()
        {
            var isFail = false;

            for (int i = 0; i < _slotsData.Length; i++)
            {
                if (_slotsData[i].ArrowDirection.Value != _slotsData[i].Decision)
                {
                    isFail = true;
                    break;
                }
            }

            if (!isFail)
            {
                _saveData.IsDecision = true;
                GameData.EffectSoundPlayer.Play(GameData.AssetProvider.HypnosisSound);
                var dictionary = new Dictionary<string, string>() { {"Puzzle", _internalKeyValue} };
                YandexMetrica.Send("Puzzle", dictionary);
                _event.Invoke();
            }
            
            return !isFail;
        }
    }
}