using System.Collections;
using System.Collections.Generic;
using MoreMountains.Feedbacks;
using PixelCrushers.DialogueSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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

        [SerializeField]
        private Image _icon, _frame;

        [SerializeField]
        private TMP_Text _nameLabel, _descriptionLabel;

        [SerializeField]
        private Button _useButton;
        
        public override void Activate(bool isActive)
        {
            base.Activate(isActive);

            HideItemUI();

            if (isActive)
            {
                Lua.Run("Variable[\"IsHackerMask\"] = true");
                Lua.Run("Variable[\"IsPrisonKey\"] = true");
                
                _selectPlayer.PlayFeedbacks();

                var assetProvider = GameData.AssetProvider;
                var itemConfigs = assetProvider.ItemsConfigContainer.GetAvailableItems();

                for (int i = 0; i < itemConfigs.Length; i++)
                {
                    var model = itemConfigs[i];
                    var slot = Instantiate(assetProvider.InventorySlotPrefab, _container);
                    slot.Model = model;
                    int rowIndex = itemConfigs.Length - i - 1;
                    int columnIndex = 0;
                    slot.SetSelected(false);
                    _slots.Add(new Vector2(columnIndex, rowIndex), slot);
                }
            
                _currentIndex = new Vector2(0, itemConfigs.Length - 1);
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
            HideItemUI();
            StartCoroutine(AwaitSelect());
        }

        private IEnumerator AwaitSelect()
        {
            yield return null;
            base.Select();
            _slots[_currentIndex].SetSelected(true);
            ShowItemUI();
        }

        public override void UnSelect()
        {
            base.Select();
            _slots[_currentIndex].SetSelected(false);
            HideItemUI();
        }
        
        public override void OnSubmitDown()
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

                    ShowItemUI();
                }
            }
            else
            {
                if (direction.y == 1 && _currentIndex.y == _slots.Count - 1)
                {
                    UnSelect();
                    _isSelect = false;
                    _gameplayMenu.Select();
                }
            }
        }

        private void HideItemUI()
        {
            _icon.gameObject.SetActive(false);
            _nameLabel.gameObject.SetActive(false);
            _descriptionLabel.gameObject.SetActive(false);
            _useButton.gameObject.SetActive(false);
            _frame.gameObject.SetActive(false);
        }

        private void ShowItemUI()
        {
            _icon.gameObject.SetActive(true);
            _nameLabel.gameObject.SetActive(true);
            _descriptionLabel.gameObject.SetActive(true);
            _frame.gameObject.SetActive(true);
                    
            var config = ((InventorySlotViewModel)_currentSlot).Model;
            _icon.sprite = config.Icon;

            if (config is IUsable usable)
            {
                //usable.Use();
                _useButton.gameObject.SetActive(true);
            }
            else
            {
                _useButton.gameObject.SetActive(false);
            }
        }
    }
}