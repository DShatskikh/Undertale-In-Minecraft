using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game
{
    public class KeySlotViewModel : BaseSlotController
    {
        [SerializeField]
        private InputActionReference m_Action;

        [SerializeField]
        private string m_BindingId;

        [SerializeField]
        private ResetKeyButton _resetKeyButton;
        
        [SerializeField]
        private KeyButton _keyButton;
        
        private KeySlotView _view;
        private bool _isSelect;
        private bool _isRight;
        private SettingKeyScreen _settingKeyScreen;

        public string BindingId => m_BindingId;
        public InputActionReference InputActionReference => m_Action;

        public void Init(SettingKeyScreen settingKeyScreen)
        {
            _settingKeyScreen = settingKeyScreen;
            _resetKeyButton.Init(settingKeyScreen, this);
            _keyButton.Init(settingKeyScreen,this);
        }
        
        private void Awake()
        {
            _view = GetComponent<KeySlotView>();
        }

        private void Start()
        {
            _view.Init(this);
            UpdateBindingDisplay();
        }

        public override void Select()
        {
            _settingKeyScreen.SelectSlot(this);
        }
        
        public override void Use()
        {
            _settingKeyScreen.OnSubmitUp();
        }
        
        public void UpdateBindingDisplay()
        {
            _view.UpdateBindingDisplay(m_Action, m_BindingId);
        }
        
        public override void SetSelected(bool isSelect)
        {
            _isSelect = isSelect;
            _view.SetSelect(isSelect, _isRight);
        }

        public void SetRight(bool isRight)
        {
            _isRight = isRight;
            _view.SetSelect(_isSelect, _isRight);
        }
    }
}