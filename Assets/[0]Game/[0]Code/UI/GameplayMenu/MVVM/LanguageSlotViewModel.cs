using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game
{
    public class LanguageSlotViewModel : BaseSlotController
    {
        [SerializeField]
        private TMP_Dropdown _dropdown;

        [SerializeField]
        private SettingScreen _settingScreen;
        
        private LanguageSlotView _view;
        private bool _isShowDropdown;
        private bool _isSelect;
        private int _selectIndex;

        private void Awake()
        {
            _view = GetComponent<LanguageSlotView>();
            _dropdown.onValueChanged.AddListener(OnItemSelect);
        }
        
        private void OnDestroy()
        {
            _dropdown.onValueChanged.RemoveListener(OnItemSelect);
        }

        private void Start()
        {
            _view.Init();
        }

        public override void SetSelected(bool isSelect)
        {
            _isSelect = isSelect;
            _view.Upgrade(isSelect, false);

            if (isSelect)
            {
                
            }
            else
            {
                GameData.PlayerInput.actions["Submit"].performed -= OnSubmitPerformed;
                GameData.PlayerInput.actions["Move"].performed -= OnMovePerformed;
            }
        }

        private void OnSubmitPerformed(InputAction.CallbackContext obj)
        {
            Debug.Log("OnSubmitPerformed");
            _dropdown.value = _selectIndex;
            _dropdown.Show();
            OnItemSelect(_selectIndex);
            
            GameData.PlayerInput.actions["Submit"].performed -= OnSubmitPerformed;
            GameData.PlayerInput.actions["Move"].performed -= OnMovePerformed;
        }

        private void OnMovePerformed(InputAction.CallbackContext obj)
        {
            if (!_isShowDropdown)
                return;
            
            Select(_selectIndex - (int)obj.ReadValue<Vector2>().y);
        }

        public void Click()
        {
            _isSelect = true;
            _isShowDropdown = true;

            _view.Upgrade(_isSelect, _isShowDropdown);
            _dropdown.Hide();
            StartCoroutine(AwaitClick());
        }

        private IEnumerator AwaitClick()
        {
            yield return null;
            Select(_selectIndex);

            Debug.Log("Click");
            GameData.PlayerInput.actions["Submit"].performed += OnSubmitPerformed;
            GameData.PlayerInput.actions["Move"].performed += OnMovePerformed;
        }
        
        private void Select(int index)
        {
            var items = _dropdown.GetComponentsInChildren<DropdownItem>();

            if (index == -1)
                index = items.Length - 1;
            
            if (index >= items.Length)
                index = items.Length - 1;
            else if (index < 0)
                index = 0;
            
            for (int i = 0; i < items.Length; i++) 
                items[i].Select(i == index);

            _selectIndex = index;
        }
        
        private void OnItemSelect(int value)
        {
            Select(value);
            _isShowDropdown = false;
            _view.Upgrade(_isSelect, _isShowDropdown);
            _settingScreen.Select();
        }
    }
}