using System;
using System.Collections;
using System.Collections.Generic;
using MoreMountains.Feedbacks;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace Game
{
    public class GameplayMenu : MenuPanelBase
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
        private InventoryScreen _inventory;
        
        [SerializeField]
        private GuideScreen guide;
        
        [SerializeField]
        private SettingScreen _setting;
        
        [SerializeField]
        private ExitScreen _exit;
        
        private bool _isShow;
        private bool _isProcess;
        private MenuPanelBase _currentScreen;

        private bool IsCanShow() => GameData.CharacterController.enabled;

        private void OnEnable()
        {
            GameData.PlayerInput.actions["Menu"].performed += OnMenuPerformed;
        }

        private void OnDisable()
        {
            GameData.PlayerInput.actions["Menu"].performed -= OnMenuPerformed;
        }

        public override void OnUpdate() { }

        public override void Select()
        {
            base.Select();
            _currentSlot.SetSelected(true);
        }

        public override void UnSelect()
        {
            base.UnSelect();
            _currentSlot.SetSelected(false);
        }

        private void OnMenuPerformed(InputAction.CallbackContext obj)
        {
            if (!_isProcess)
            {
                if (_isShow)
                {
                    StartCoroutine(AwaitHide());
                }
                else 
                    TryShow();
            }
        }

        public bool TryShow()
        {
            if (IsCanShow())
            {
                StartCoroutine(AwaitShow());
                return true;
            }
            
            return false;
        }
        
        public override void OnSubmitDown() { }

        public override void OnCancel() { }

        private IEnumerator AwaitShow()
        {
            GameData.CharacterController.enabled = false;
            _menu.SetActive(true);
            
            if (!_currentScreen)
                _currentScreen = _inventory;
            
            _currentScreen.Activate(false);

            var assetProvider = GameData.AssetProvider;
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
            
            _currentScreen.Activate(true);
            Select();
        }

        private IEnumerator AwaitHide()
        {
            GameData.CharacterController.enabled = true;
            
            if (_currentScreen)
                _currentScreen.Activate(false);
            
            _isShow = false;
            _isProcess = true;
            yield return _hidePlayer.PlayFeedbacksCoroutine(Vector3.zero);
            _isProcess = false;
            
            UnSelect();
            
            foreach (var slot in _slots) 
                Destroy(slot.Value.gameObject);

            _slots = new Dictionary<Vector2, BaseSlotController>();
            
            _menu.SetActive(false);
        }

        public override void OnSlotIndexChanged(Vector2 direction)
        {
            if (!_isSelect || !_isShow)
                return;
            
            StartCoroutine(AwaitOnSlotIndexChanged(direction));
        }

        private IEnumerator AwaitOnSlotIndexChanged(Vector2 direction)
        {
            var startIndex = _currentIndex;
            base.OnSlotIndexChanged(direction);

            if (startIndex != _currentIndex)
            {
                var model = ((GameplayMenuSlotViewModel)_slots[_currentIndex]).Model.Config;
                var loadTextCommand = new LoadTextCommand(model.Name);
                yield return loadTextCommand.Await().ContinueWith(() => _selectSlotLabel.text = loadTextCommand.Result);
            
                if (_currentScreen)
                    _currentScreen.Activate(false);
            
                _currentScreen = model.SlotType switch
                {
                    GameplayMenuSlotType.Inventory => _inventory,
                    GameplayMenuSlotType.Endings => guide,
                    GameplayMenuSlotType.Setting => _setting,
                    GameplayMenuSlotType.Exit => _exit,
                    _ => throw new ArgumentOutOfRangeException()
                };
            
                _currentScreen.Activate(true);
            }
            else if (direction.y == -1)
            {
                UnSelect();
                _currentScreen.Select();
            }
            
            print("AwaitOnSlotIndexChanged");
        }
    }
}