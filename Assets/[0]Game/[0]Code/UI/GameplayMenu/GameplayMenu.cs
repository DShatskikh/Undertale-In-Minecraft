using System;
using System.Collections;
using System.Collections.Generic;
using Game.Commands;
using MoreMountains.Feedbacks;
using TMPro;
using UnityEngine;

namespace Game
{
    public class GameplayMenu : UIPanelBase
    {
        [SerializeField]
        private Transform _container;

        [SerializeField]
        private TMP_Text _selectSlotLabel;

        [SerializeField]
        private MMF_Player _showPlayer, _hidePlayer;

        [SerializeField]
        private GameObject _menu;

        [SerializeField]
        private GameObject _inventory, _endings, _setting, _exit;
        
        private bool _isShow;
        private bool _isProcess;
        private GameObject _currentScreen;

        public override void OnUpdate()
        {
            if (Input.GetButtonUp("Menu") && !_isProcess) 
                StartCoroutine(_isShow ? AwaitHide() : AwaitShow());
        }
        
        public override void OnSubmit()
        {
            GameData.EffectSoundPlayer.Play(GameData.AssetProvider.ClickSound);
            var slot = (ActSlotController)_currentSlot;
            slot.Model.IsSelectedOnce = true;
            GameData.Battle.Turn(slot.Model.Act);
            gameObject.SetActive(false);
        }

        public override void OnCancel() { }

        private IEnumerator AwaitShow()
        {
            _menu.SetActive(true);
            
            if (!_currentScreen)
                _currentScreen = _inventory;
            
            _currentScreen.SetActive(false);
            
            var assetProvider = GameData.AssetProvider;
            
            Debug.Log($"{gameObject.name}: {assetProvider.GameplayMenuConfigs}");

            var slotsData = assetProvider.GameplayMenuConfigs;

            for (int i = 0; i < slotsData.Length; i++)
            {
                var model = new GameplayMenuSlotModel(slotsData[i]);
                var slot = Instantiate(assetProvider.GameplayMenuSlotPrefab, _container);
                slot.Model = model;
                int rowIndex = 0;
                int columnIndex = i;
                slot.SetSelected(false);
                _slots.Add(new Vector2(columnIndex, rowIndex), slot);
            }
            
            _currentIndex = new Vector2(0, 0);
            _slots[_currentIndex].SetSelected(true);
            
            _isShow = true;
            _isProcess = true;
            yield return _showPlayer.PlayFeedbacksCoroutine(Vector3.zero);
            _isProcess = false;
            
            _currentScreen.SetActive(true);
        }

        private IEnumerator AwaitHide()
        {
            if (_currentScreen)
                _currentScreen.SetActive(false);
            
            _isShow = false;
            _isProcess = true;
            yield return _hidePlayer.PlayFeedbacksCoroutine(Vector3.zero);
            _isProcess = false;
            
            foreach (var slot in _slots) 
                Destroy(slot.Value.gameObject);

            _slots = new Dictionary<Vector2, BaseSlotController>();
            
            _menu.SetActive(false);
        }

        public override void OnSlotIndexChanged(Vector2 direction)
        {
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
                    StartCoroutine(AwaitOnSlotIndexChanged(direction));
                }
            }
        }

        private IEnumerator AwaitOnSlotIndexChanged(Vector2 direction)
        {
            var model = ((GameplayMenuSlotViewModel)_slots[_currentIndex]).Model.Config;
            var loadTextCommand = new LoadTextCommand(model.Name);
            yield return loadTextCommand.Await().ContinueWith(() => _selectSlotLabel.text = loadTextCommand.Result);

            if (_currentScreen)
                _currentScreen.SetActive(false);

            _currentScreen = model.SlotType switch
            {
                GameplayMenuSlotType.Inventory => _inventory,
                GameplayMenuSlotType.Endings => _endings,
                GameplayMenuSlotType.Setting => _setting,
                GameplayMenuSlotType.Exit => _exit,
                _ => throw new ArgumentOutOfRangeException()
            };

            _currentScreen.SetActive(true);
        }
    }
}