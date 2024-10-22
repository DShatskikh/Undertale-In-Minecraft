using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Localization;

namespace Game
{
    public class PuzzleLevers : UseObject
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
        private bool _isDecision;

        public List<ReactiveProperty<bool>> CurrentProgress => _currentProgress;
        
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