using System;
using System.Collections;
using System.Collections.Generic;
using MoreMountains.Feedbacks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game
{
    public class ExitScreen : MenuPanelBase
    {
        [SerializeField]
        private MMF_Player _selectPlayer;
        
        [SerializeField]
        private GameplayMenu _gameplayMenu;

        [SerializeField]
        private Transform _container;

        public override void Activate(bool isActive)
        {
            base.Activate(isActive);

            if (isActive)
            {
                _selectPlayer.PlayFeedbacks();
                
                var slotsData = GameData.AssetProvider.ExitSlotConfigs;
                var assetProvider = GameData.AssetProvider;

                for (int i = 0; i < slotsData.Length; i++)
                {
                    var slot = Instantiate(assetProvider.ExitSlotPrefab, _container);
                    slot.Model = slotsData[i];
                    slot.Init(this);
                    int rowIndex = slotsData.Length - i - 1;
                    int columnIndex = 0;
                    _slots.Add(new Vector2(columnIndex, rowIndex), slot);
                }

                _currentIndex = new Vector2(0, slotsData.Length - 1);
            }
            else
            {
                foreach (var slot in _slots) 
                    Destroy(slot.Value.gameObject);

                _slots = new Dictionary<Vector2, BaseSlotController>();
            }
        }
        
        public override void Select()
        {
            base.Select();
            _currentSlot.SetSelected(true);
            GameData.EffectSoundPlayer.Play(GameData.AssetProvider.SelectSound);
        }

        public override void UnSelect()
        {
            base.UnSelect();
            _currentSlot.SetSelected(false);
        }
        
        public override void OnSubmitDown()
        {
            if (!_isSelect)
                return;
            
            switch (((ExitSlotViewModel)_currentSlot).Model.ExitSlotType)
            {
                case ExitSlotType.Menu:
                    SceneManager.LoadScene(0);
                    break;
                case ExitSlotType.Desktop:
                    Application.Quit();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
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
        
        public override void SelectSlot(BaseSlotController slotViewModel)
        {
            base.SelectSlot(slotViewModel);
            _gameplayMenu.UnSelect();
            Select();
        }
        
        public void SelectZero()
        {
            UnSelect();
            _currentIndex = new Vector2(0, _slots.Count - 1);
            Select();
        }
    }
}