using System;
using System.Collections.Generic;
using PixelCrushers;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Localization;
using YG;

namespace Game
{
    public class PuzzleLevers : Saver, IUseObject
    {
        [SerializeField]
        private PuzzleLeversView _view;

        [SerializeField]
        private bool[] _decision;

        [SerializeField]
        private LocalizedString _localizedString;
        
        [SerializeField]
        private UnityEvent _event;
        
        private List<ReactiveProperty<bool>> _currentProgress;
        private Data _saveData = new();
        
        [Serializable]
        public class Data
        {
            public bool IsDecision;
        }
        
        public List<ReactiveProperty<bool>> CurrentProgress => _currentProgress;

        public override void Start()
        {
            base.Start();
        }

        public override string RecordData()
        {
            return SaveSystem.Serialize(_saveData);
        }

        public override void ApplyData(string s)
        {
            var data = SaveSystem.Deserialize(s, _saveData);
            _saveData = data;

            if (_saveData.IsDecision)
                _event.Invoke();
        }

        public void Use()
        {
            GameData.CharacterController.enabled = false;

            if (!_saveData.IsDecision)
            {
                if (_currentProgress == null)
                {
                    _currentProgress = new List<ReactiveProperty<bool>>();

                    foreach (var isOn in _decision) 
                        _currentProgress.Add(new ReactiveProperty<bool>());
                
                    _view.SetModel(this);
                }
            
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

            for (int i = 0; i < _decision.Length; i++)
            {
                if (_currentProgress[i].Value != _decision[i])
                {
                    isFail = true;
                    break;
                }
            }

            if (!isFail)
            {
                _saveData.IsDecision = true;
                GameData.EffectSoundPlayer.Play(GameData.AssetProvider.PistonSound);
                var dictionary = new Dictionary<string, string>() { {"Puzzle", _internalKeyValue} };
                YandexMetrica.Send("Puzzle", dictionary);
                _event.Invoke();
            }
            
            return !isFail;
        }

        public void SetIsDecision(bool isDecision)
        {
            _saveData.IsDecision = isDecision;
        }
    }
}