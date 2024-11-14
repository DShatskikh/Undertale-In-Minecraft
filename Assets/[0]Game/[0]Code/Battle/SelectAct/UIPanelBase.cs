using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game
{
    public abstract class UIPanelBase : MonoBehaviour
    {
        protected Dictionary<Vector2, BaseSlotController> _slots = new Dictionary<Vector2, BaseSlotController>();
        protected Vector2 _currentIndex;

        protected BaseSlotController _currentSlot => _slots[_currentIndex];
        public BaseSlotController CurrentSlot => _currentSlot;

        public virtual void Activate(bool isActive)
        {
            gameObject.SetActive(isActive);
            Register(isActive);
        }

        private void OnDestroy()
        {
            Register(false);
        }

        protected void Register(bool value)
        {
            if (value)
            {
                GameData.PlayerInput.actions["Move"].performed += OnMovePerformed;
                GameData.PlayerInput.actions["Submit"].performed += OnSubmitPerformed;
                GameData.PlayerInput.actions["Submit"].canceled += OnSubmitCanceled;
                GameData.PlayerInput.actions["Cancel"].performed += OnCancelPerformed;
            }
            else
            {
                if (!GameData.PlayerInput)
                    return;
                
                GameData.PlayerInput.actions["Move"].performed -= OnMovePerformed;
                GameData.PlayerInput.actions["Submit"].performed -= OnSubmitPerformed;
                GameData.PlayerInput.actions["Submit"].canceled -= OnSubmitCanceled;
                GameData.PlayerInput.actions["Cancel"].performed -= OnCancelPerformed;
            }
        }

        private void OnMovePerformed(InputAction.CallbackContext obj)
        {
            if (obj.ReadValue<Vector2>() != Vector2.zero)
                OnSlotIndexChanged(obj.ReadValue<Vector2>());
        }

        private void OnSubmitPerformed(InputAction.CallbackContext obj)
        {
            OnSubmitDown();
        }

        private void OnSubmitCanceled(InputAction.CallbackContext obj)
        {
            OnSubmitUp();
        }

        private void OnCancelPerformed(InputAction.CallbackContext obj)
        {
            OnCancel();
        }

        private void Update()
        {
            OnUpdate();
        }

        public abstract void OnSubmitDown();

        public abstract void OnSubmitUp();

        public abstract void OnCancel();

        public virtual void OnUpdate() { }

        public virtual void OnSlotIndexChanged(Vector2 direction)
        {
            if (direction == Vector2.zero)
                return;
            
            var newIndex = _currentIndex + direction;
            
            if (_slots.TryGetValue(newIndex, out var controller))
            {
                var oldVM = _slots[_currentIndex];
                
                if (controller != null && oldVM != controller)
                {
                    oldVM.SetSelected(false);
                    controller.SetSelected(true);
                    _currentIndex = newIndex;
                    
                    GameData.EffectSoundPlayer.Play(GameData.AssetProvider.SelectSound);
                }
            }
        }
    }
}