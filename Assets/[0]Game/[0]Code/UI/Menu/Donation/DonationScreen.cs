using System.Collections.Generic;
using MoreMountains.Feedbacks;
using UnityEngine;
using UnityEngine.Serialization;
using YG;
using YG.Utils.Pay;

namespace Game
{
    public class DonationScreen : MenuPanelBase
    {
        [SerializeField]
        private MMF_Player _selectPlayer;

        [SerializeField]
        private Transform _container;

        [SerializeField]
        private MenuPanelBase _menu;

        //[SerializeField]
        //private DonationSlotConfig[] _configs;

        public override void Activate(bool isActive)
        {
            base.Activate(isActive);

            if (isActive)
            {
                _selectPlayer.PlayFeedbacks();
                var assetProvider = GameData.AssetProvider;

                foreach (var purchase in YandexGame.purchases)
                {
                    var slot = Instantiate(assetProvider.DonationSlotPrefab, _container);
                    slot.Init(this, purchase);
                    int rowIndex = _slots.Count;
                    int columnIndex = 0;
                    _slots.Add(new Vector2(columnIndex, rowIndex), slot);
                }

                _currentIndex = new Vector2(0, _slots.Count - 1);
                _currentSlot.SetSelected(true);
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
            
            if (_currentSlot)
                _currentSlot.SetSelected(false);
        }
        
        public override void OnSubmitDown()
        {
            if (!_isSelect)
                return;
            
        }

        public override void OnSubmitUp()
        {
            print(_currentSlot);
            YandexGame.BuyPayments(((DonationSlotViewModel)_currentSlot).Model.id);
            Invoke("OnCancel", 0f);
        }

        public override void OnCancel()
        {
            _menu.Activate(true);
            Activate(false);
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
                }
            }
        }
        
        public override void SelectSlot(BaseSlotController slotViewModel)
        {
            base.SelectSlot(slotViewModel);
            Select();
        }
    }
}