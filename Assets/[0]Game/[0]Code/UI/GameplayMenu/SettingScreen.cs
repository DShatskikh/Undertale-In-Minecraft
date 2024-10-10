using System.Collections.Generic;
using MoreMountains.Feedbacks;
using UnityEngine;

namespace Game
{
    public class SettingScreen : MenuPanelBase
    {
        [SerializeField]
        private MMF_Player _selectPlayer;
        
        [SerializeField]
        private GameplayMenu _gameplayMenu;
        
        public override void Activate(bool isActive)
        {
            base.Activate(isActive);

            if (isActive)
            {
                _selectPlayer.PlayFeedbacks();
            }
        }

        public override void Select()
        {
            base.Select();
            
            var slots = GetComponentsInChildren<BaseSlotController>();
                    
            for (int i = 0; i < slots.Length; i++)
            {
                _slots.Add(new Vector2(0, slots.Length - i - 1), slots[i]);
                slots[i].SetSelected(false);
            }
                
            slots[^1].SetSelected(true);
        }

        public override void UnSelect()
        {
            base.UnSelect();
            _currentSlot.SetSelected(false);
            _slots = new Dictionary<Vector2, BaseSlotController>();
        }

        public override void OnSubmit()
        {
            
        }

        public override void OnCancel()
        {
            
        }
        
        public override void OnSlotIndexChanged(Vector2 direction)
        {
            if (!_isSelect)
                return;
            
            var newIndex = _currentIndex + direction;
            
            if (_slots.TryGetValue(newIndex, out var controller))
            {
                if (controller != null)
                {
                    controller.SetSelected(true);
                    var oldVM = _slots[_currentIndex];
                    oldVM.SetSelected(false);
                    _currentIndex = newIndex;
                    
                    GameData.EffectSoundPlayer.Play(GameData.AssetProvider.SelectSound);
                }
            }
            else
            {
                if (direction is { y: 1, x: 0 } && _currentIndex.y == _slots.Count - 1)
                {
                    UnSelect();
                    _isSelect = false;
                    _gameplayMenu.Select();
                }
            }
        }
    }
}