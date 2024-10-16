using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.Localization.Settings;

namespace Game
{
    public class LanguageDropdown : UIPanelBase, IPointerUpHandler
    {
        [SerializeField]
        private TMP_Dropdown _dropdown;

        private bool _isActive;
        private int _selectIndex;
        private SettingScreen _settingScreen;

        public static LanguageDropdown GetInstance;
        private List<DropdownItem> _elements = new List<DropdownItem>();

        public override void OnUpdate()
        {
            if (Input.GetMouseButtonUp(0) && _isActive)
                Activate(false);
        }

        public void Init(SettingScreen settingScreen)
        {
            _settingScreen = settingScreen;
            GetInstance = this;
        }

        public override void Activate(bool isActive)
        {
            if (isActive)
            {
                ShowDropDown(true);
            }
            else
            {
                _dropdown.Hide();   
                _dropdown.onValueChanged.RemoveListener(OnValueChanged);
                _isActive = false;
                Register(false);
                _settingScreen.Select();
            }
        }

        private void ShowDropDown(bool isShow)
        {
            StartCoroutine(AwaitShowDropDown(isShow));
        }
        
        private IEnumerator AwaitShowDropDown(bool isShow)
        {
            _elements = new List<DropdownItem>();
            
            if (isShow)
                _dropdown.Show();
            
            yield return null;

            _elements = FindObjectsByType<DropdownItem>(FindObjectsInactive.Exclude, FindObjectsSortMode.InstanceID).ToList();
            
            for (var index = 0; index < _elements.Count; index++)
            {
                var element = _elements[index];
                print(index);
                element.SetIndex(index);
            }

            SelectSlot(0);
            
            _dropdown.onValueChanged.AddListener(OnValueChanged);
            _isActive = true;
            Register(true);
        }
        
        public override void OnSubmitDown()
        {
            
        }

        public override void OnSubmitUp()
        {
            Activate(false);
        }

        public override void OnCancel()
        {
            Activate(false);
        }

        public override void OnSlotIndexChanged(Vector2 direction)
        {
            _selectIndex = Mathf.Clamp(_selectIndex - (int)direction.y, 0, _elements.Count - 1);
            SelectSlot(_selectIndex);
        }

        public void SelectSlot(int index)
        {
            _selectIndex = index;
            
            for (int i = 0; i < _elements.Count; i++) 
                _elements[i].Select(i == _selectIndex);
            
            GameData.EffectSoundPlayer.Play(GameData.AssetProvider.SelectSound);
        }
        
        private void OnValueChanged(int value)
        {
            Activate(false);
            GameData.EffectSoundPlayer.Play(GameData.AssetProvider.ClickSound);
            StartCoroutine(AwaitChangeLanguage(value));
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (!_isActive)
                ShowDropDown(false);
        }


        private IEnumerator AwaitChangeLanguage(int id)
        {
            yield return LocalizationSettings.InitializationOperation;
            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[id];
            
            print($"id: {id}");
        }
    }
}