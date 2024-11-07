using System;
using PixelCrushers.DialogueSystem;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Localization;

namespace Game
{
    public class PuzzleArrows : UseObject
    {
        [SerializeField]
        private PuzzleArrowView _view;

        [SerializeField]
        private PuzzleArrowSlotData[] _slotsData;

        [SerializeField]
        private LocalizedString _localizedString;

        [SerializeField]
        private UnityEvent _event;
        
        private bool _isDecision;
        public PuzzleArrowSlotData[] SlotsData => _slotsData;

        private void Start()
        {
            _isDecision = Lua.IsTrue("Variable[\"IsArrowPuzzle\"] == true");

            if (_isDecision)
                _event.Invoke();

            foreach (var slotData in _slotsData)
            {
                slotData.ArrowDirection.Changed += (direction) => OnArrowChanged(direction, slotData);
                
                if (_isDecision)
                    slotData.ArrowDirection.Value = slotData.Decision;
                else
                    slotData.ArrowDirection.Value = slotData.StartArrowDirection;
                
                slotData.ArrowSpriteRenderer.sprite = slotData.View;
            }
        }

        private void OnArrowChanged(ArrowDirection direction, PuzzleArrowSlotData data)
        {
            data.ArrowCenter.eulerAngles = direction.GetAngle() - data.StartArrowDirection.GetAngle();
        }
        
        public override void Use()
        {
            GameData.CharacterController.enabled = false;

            if (!_isDecision)
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
                _isDecision = true;
                GameData.EffectSoundPlayer.Play(GameData.AssetProvider.HypnosisSound);
                Lua.Run("Variable[\"IsArrowPuzzle\"] = true");
                _event.Invoke();
            }
            
            return !isFail;
        }
    }
}