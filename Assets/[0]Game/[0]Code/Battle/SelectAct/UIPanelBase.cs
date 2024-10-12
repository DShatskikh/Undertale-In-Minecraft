using System;
using System.Collections.Generic;
using UnityEngine;

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
        }

        private void Update()
        {
            if (Input.GetButtonDown("Submit"))
                OnSubmit();
                    
            if (Input.GetButtonDown("Cancel")) 
                OnCancel();

            var direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

            if (Input.GetButtonDown("Horizontal") || Input.GetButtonDown("Vertical")) 
                OnSlotIndexChanged(direction);

            OnUpdate();
        }

        public abstract void OnSubmit();
        public abstract void OnCancel();

        public virtual void OnUpdate() { }

        public virtual void OnSlotIndexChanged(Vector2 direction)
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
                }
            }
        }
    }
}