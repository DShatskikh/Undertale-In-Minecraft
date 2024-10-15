using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game
{
    public class LanguageSlotViewModel : BaseSlotController
    {
        [SerializeField]
        private LanguageDropdown _languageDropdown;
        
        private SettingScreen _settingScreen;
        private LanguageSlotView _view;

        public void Init(SettingScreen settingScreen)
        {
            _settingScreen = settingScreen;
            _languageDropdown.Init(_settingScreen);
        }

        private void Awake()
        {
            _view = GetComponent<LanguageSlotView>();
        }

        private void Start()
        {
            _view.Init(this);
        }

        public override void SetSelected(bool isSelect)
        {
            _view.Upgrade(isSelect, false);
        }

        public void Click()
        {
            _settingScreen.UnSelect();
            _languageDropdown.Activate(true);
        }

        public override void Select()
        {
            _settingScreen.SelectSlot(this);
        }

        public override void Use()
        {
            _settingScreen.OnSubmitUp();
        }

        public override void SubmitDown()
        {
            //_view.SubmitDown();
        }

        public override void SubmitUp()
        {
            //_view.SubmitUp();
        }
    }
}