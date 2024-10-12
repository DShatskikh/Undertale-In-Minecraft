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

        private KeySlotView _view;
        private bool _isSelect;
        private bool _isRight;

        public string BindingId => m_BindingId;
        public InputActionReference InputActionReference => m_Action;
        
        private void Awake()
        {
            _view = GetComponent<KeySlotView>();
        }

        private void Start()
        {
            //_view.Init(this, _keyHash);
            UpdateBindingDisplay();
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