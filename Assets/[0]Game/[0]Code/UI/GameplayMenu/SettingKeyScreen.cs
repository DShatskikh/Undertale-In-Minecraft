using System;
using System.Collections.Generic;
using MoreMountains.Feedbacks;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game
{
    public class SettingKeyScreen : MenuPanelBase
    {
        [SerializeField]
        private MMF_Player _selectPlayer;
        
        [SerializeField]
        private SettingScreen _settingScreen;

        [SerializeField]
        private RebindKey _rebindKey;

        private InputActionRebindingExtensions.RebindingOperation m_RebindOperation;
        private KeySlotViewModel _keySlot => (KeySlotViewModel)CurrentSlot;
        private bool _isRight;

        public override void Activate(bool isActive)
        {
            base.Activate(isActive);

            if (isActive)
            {
                _selectPlayer.PlayFeedbacks();
                
                _slots = new Dictionary<Vector2, BaseSlotController>();
                var slots = GetComponentsInChildren<BaseSlotController>();
                    
                for (int i = 0; i < slots.Length; i++)
                {
                    _slots.Add(new Vector2(0, slots.Length - i - 1), slots[i]);
                    slots[i].SetSelected(false);
                }

                _currentIndex = new Vector2(0, _slots.Count - 1);
                Select();
                _currentSlot.SetSelected(true);
            }
            else
            {
                _currentSlot.SetSelected(false);
                _slots = new Dictionary<Vector2, BaseSlotController>();
            }
        }

        public override void Select()
        {
            base.Select();
            
        }

        public override void UnSelect()
        {
            base.UnSelect();

        }

        public override void OnSubmit()
        {
            if (!_isSelect)
                return;

            if (_currentSlot is KeySlotViewModel)
            {
                UnSelect();

                if (!_isRight)
                    StartInteractiveRebind(_keySlot.InputActionReference, _keySlot.BindingId);
                else
                    ResetToDefault(_keySlot);
            }
            else
            {
                foreach (var slot in _slots)
                {
                    if (slot.Value is KeySlotViewModel keySlot)
                        ResetToDefault(keySlot);
                }
            }
        }

        public override void OnSlotIndexChanged(Vector2 direction)
        {
            if (!_isSelect)
                return;
            
            base.OnSlotIndexChanged(direction);

            if (_currentSlot is KeySlotViewModel)
            {
                _isRight = direction.x switch
                {
                    > 0 => true,
                    < 0 => false,
                    _ => _isRight
                };

                _keySlot.SetRight(_isRight);
            }
        }

        public override void OnCancel()
        {
            if (!_isSelect)
                return;
            
            Activate(false);
            UnSelect();
            
            _settingScreen.Activate(true);
            _settingScreen.Select();
        }

        private bool ResolveActionAndBinding(out InputAction action, out int bindingIndex, InputActionReference m_Action, string m_BindingId)
        {
            bindingIndex = -1;
            
            action = m_Action?.action;
            if (action == null)
                return false;

            if (string.IsNullOrEmpty(m_BindingId))
                return false;

            // Look up binding index.
            var bindingId = new Guid(m_BindingId);
            bindingIndex = action.bindings.IndexOf(x => x.id == bindingId);
            if (bindingIndex == -1)
            {
                Debug.LogError($"Cannot find binding with ID '{bindingId}' on '{action}'", this);
                return false;
            }

            return true;
        }

        public void StartInteractiveRebind(InputActionReference m_Action, string m_BindingId)
        {
            if (!ResolveActionAndBinding(out var action, out var bindingIndex, m_Action, m_BindingId))
                return;

            // If the binding is a composite, we need to rebind each part in turn.
            if (action.bindings[bindingIndex].isComposite)
            {
                var firstPartIndex = bindingIndex + 1;
                if (firstPartIndex < action.bindings.Count && action.bindings[firstPartIndex].isPartOfComposite)
                    PerformInteractiveRebind(action, firstPartIndex, allCompositeParts: true);
            }
            else
            {
                PerformInteractiveRebind(action, bindingIndex);
            }
        }

        private void PerformInteractiveRebind(InputAction action, int bindingIndex, bool allCompositeParts = false)
        {
            m_RebindOperation?.Cancel(); // Will null out m_RebindOperation.

            void CleanUp()
            {
                m_RebindOperation?.Dispose();
                m_RebindOperation = null;
                action.Enable();
            }

            //Fixes the "InvalidOperationException: Cannot rebind action x while it is enabled" error
            action.Disable();

            // Configure the rebind.
            m_RebindOperation = action.PerformInteractiveRebinding(bindingIndex)
                .OnCancel(
                    operation =>
                    {
                        //m_RebindStopEvent?.Invoke(this, operation);
                        _rebindKey.gameObject.SetActive(false);
                        _rebindKey.UpdateBindingDisplay();
                        CleanUp();
                        Select();
                        _keySlot.UpdateBindingDisplay();
                    })
                .OnComplete(
                    operation =>
                    {
                        _rebindKey.gameObject.SetActive(false);
                        //m_RebindStopEvent?.Invoke(this, operation);
                        _rebindKey.UpdateBindingDisplay();
                        CleanUp();

                        // If there's more composite parts we should bind, initiate a rebind
                        // for the next part.
                        if (allCompositeParts)
                        {
                            var nextBindingIndex = bindingIndex + 1;
                            if (nextBindingIndex < action.bindings.Count && action.bindings[nextBindingIndex].isPartOfComposite)
                                PerformInteractiveRebind(action, nextBindingIndex, true);
                        }
                        Select();
                        _keySlot.UpdateBindingDisplay();
                    });

            // If it's a part binding, show the name of the part in the UI.
            var partName = default(string);
            if (action.bindings[bindingIndex].isPartOfComposite)
                partName = $"Binding '{action.bindings[bindingIndex].name}'. ";

            // Bring up rebind overlay, if we have one.
            _rebindKey.gameObject.SetActive(true);
 
            var text = !string.IsNullOrEmpty(m_RebindOperation.expectedControlType)
                ? $"{partName}Waiting for {m_RebindOperation.expectedControlType} input..."
                : $"{partName}Waiting for input...";
            _rebindKey.SetText(text);
            
            // Give listeners a chance to act on the rebind starting.
            //m_RebindStartEvent?.Invoke(this, m_RebindOperation);
            m_RebindOperation.Start();
        }

        public void ResetToDefault(KeySlotViewModel keySlot)
        {
            if (!ResolveActionAndBinding(out var action, out var bindingIndex, keySlot.InputActionReference, keySlot.BindingId))
                return;

            if (action.bindings[bindingIndex].isComposite)
            {
                // It's a composite. Remove overrides from part bindings.
                for (var i = bindingIndex + 1; i < action.bindings.Count && action.bindings[i].isPartOfComposite; ++i)
                    action.RemoveBindingOverride(i);
            }
            else
            {
                action.RemoveBindingOverride(bindingIndex);
            }
            
            Select();
            keySlot.UpdateBindingDisplay();
        }
    }
}