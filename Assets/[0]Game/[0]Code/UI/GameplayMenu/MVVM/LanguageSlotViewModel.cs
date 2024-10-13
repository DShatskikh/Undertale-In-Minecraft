using System;
using TMPro;
using UnityEngine;

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
        }

        public void Click()
        {
            _isSelect = true;
            _isShowDropdown = true;

            if (_isShowDropdown)
            {
                _dropdown.Show();
                Select(0);
            }

            _view.Upgrade(_isSelect, _isShowDropdown);
        }

        private void Update()
        {
            if (!_isShowDropdown)
                return;
            
            if (Input.GetButtonDown("Vertical")) 
                Select(_selectIndex - (int)Input.GetAxisRaw("Vertical"));

            if (Input.GetButtonDown("Submit"))
            {
                _dropdown.value = _selectIndex;
                _dropdown.Hide();
                //_view.Upgrade(_isSelect, _isShowDropdown);
                
                //_settingScreen.Select();
                OnItemSelect(_selectIndex);
            }
        }

        private void Select(int index)
        {
            var items = _dropdown.GetComponentsInChildren<DropdownItem>();

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