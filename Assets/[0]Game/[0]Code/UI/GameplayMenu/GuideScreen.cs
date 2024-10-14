using System.Collections;
using System.Collections.Generic;
using MoreMountains.Feedbacks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class GuideScreen : MenuPanelBase
    {
        [SerializeField]
        private MMF_Player _selectPlayer;
        
        [SerializeField]
        private Image _icon, _frame;
        
        [SerializeField]
        private TMP_Text _descriptionLabel;
        
        [SerializeField]
        private GameplayMenu _gameplayMenu;

        [SerializeField]
        private Transform _container;

        public override void Activate(bool isActive)
        {
            base.Activate(isActive);
            
            HideItemUI();
            
            if (isActive)
            {
                _selectPlayer.PlayFeedbacks();

                var assetProvider = GameData.AssetProvider;
                var configs = assetProvider.GuideConfigs;

                for (int i = 0; i < configs.Length; i++)
                {
                    var model = configs[i];
                    var slot = Instantiate(assetProvider.GuideSlotPrefab, _container);
                    slot.Model = model;
                    int rowIndex = configs.Length - i - 1;
                    int columnIndex = 0;
                    slot.SetSelected(false);
                    _slots.Add(new Vector2(columnIndex, rowIndex), slot);
                }
            
                _currentIndex = new Vector2(0, configs.Length - 1);
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
                if (direction is { y: 1, x: 0 } && _currentIndex.y == _slots.Count - 1)
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
            _descriptionLabel.gameObject.SetActive(false);
            _frame.gameObject.SetActive(false);
        }

        private void ShowItemUI()
        {
            _icon.gameObject.SetActive(true);
            _descriptionLabel.gameObject.SetActive(true);
            _frame.gameObject.SetActive(true);
                    
            var config = ((GuideSlotViewModel)_currentSlot).Model;
            _icon.sprite = config.Picture;

            StartCoroutine(AwaitShowItemUI());
        }
        
        private IEnumerator AwaitShowItemUI()
        {
            var model = ((GuideSlotViewModel)_slots[_currentIndex]).Model;
            var loadTextCommand = new LoadTextCommand(model.Info);
            yield return loadTextCommand.Await().ContinueWith(() => _descriptionLabel.text = loadTextCommand.Result);
        }
    }
}