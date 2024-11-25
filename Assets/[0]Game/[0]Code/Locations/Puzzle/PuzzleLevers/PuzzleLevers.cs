using System;
using System.Collections.Generic;
using PixelCrushers.DialogueSystem;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Localization;
using YG;

namespace Game
{
    public class PuzzleLevers : UseObject
    {
        [SerializeField]
        private string _id;
        
        [SerializeField]
        private PuzzleLeversView _view;

        [SerializeField]
        private bool[] _decision;

        [SerializeField]
        private LocalizedString _localizedString;
        
        [SerializeField]
        private UnityEvent _event;
        
        private List<ReactiveProperty<bool>> _currentProgress;
        private bool _isDecision;

        public List<ReactiveProperty<bool>> CurrentProgress => _currentProgress;

        private void Start()
        {
            _isDecision = Lua.IsTrue($"Variable[\"IsArrowPuzzle_{_id}\"] == true");
            
            if (_isDecision)
                _event.Invoke();
        }

        public override void Use()
        {
            GameData.CharacterController.enabled = false;

            if (!_isDecision)
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
                _isDecision = true;
                GameData.EffectSoundPlayer.Play(GameData.AssetProvider.PistonSound);
                Lua.Run($"Variable[\"IsPuzzleLevers_{_id}\"] = true");
                var dictionary = new Dictionary<string, string>() { {"Puzzle", $"IsPuzzleLevers_{_id}"} };
                YandexMetrica.Send("Puzzle", dictionary);
                _event.Invoke();
            }
            
            return !isFail;
        }

        public void SetIsDecision(bool isDecision)
        {
            _isDecision = isDecision;
        }
    }
}