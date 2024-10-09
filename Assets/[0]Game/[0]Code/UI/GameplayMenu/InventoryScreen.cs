using System.Collections;
using System.Collections.Generic;
using MoreMountains.Feedbacks;
using UnityEngine;

namespace Game
{
    public class InventoryScreen : MenuPanelBase
    {
        [SerializeField]
        private MMF_Player _selectPlayer;

        [SerializeField]
        private Transform _container;

        [SerializeField]
        private GameplayMenu _gameplayMenu;
        
        public override void Activate(bool isActive)
        {
            base.Activate(isActive);

            if (isActive)
            {
                _selectPlayer.PlayFeedbacks();
                
                var assetProvider = GameData.AssetProvider;

                for (int i = 0; i < 5; i++)
                {
                    var model = new InventorySlotModel();
                    var slot = Instantiate(assetProvider.InventorySlotPrefab, _container);
                    slot.Model = model;
                    int rowIndex = 4 - i;
                    int columnIndex = 0;
                    slot.SetSelected(false);
                    _slots.Add(new Vector2(columnIndex, rowIndex), slot);
                }
            
                _currentIndex = new Vector2(0, 4);
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
            StartCoroutine(AwaitSelect());
        }

        private IEnumerator AwaitSelect()
        {
            yield return null;
            base.Select();
            _slots[_currentIndex].SetSelected(true);
        }

        public override void UnSelect()
        {
            base.Select();
            _slots[_currentIndex].SetSelected(false);
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
                if (direction.y == 1 && _currentIndex.y == 4)
                {
                    UnSelect();
                    _isSelect = false;
                    _gameplayMenu.Select();
                }
            }
        }
    }
}