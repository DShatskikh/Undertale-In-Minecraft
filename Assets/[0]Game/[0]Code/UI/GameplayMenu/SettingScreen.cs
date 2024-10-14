using System.Collections;
using System.Collections.Generic;
using MoreMountains.Feedbacks;
using UnityEngine;

namespace Game
{
    public class SettingScreen : MenuPanelBase
    {
        [SerializeField]
        private MMF_Player _selectPlayer;
        
        [SerializeField]
        private MenuPanelBase _menu;

        [SerializeField]
        private bool _isExitUp = true;
        
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
                    
                    if (slots[i] is VolumeSlotViewModel volumeSlot)
                        volumeSlot.Init(this);
                }

                _currentIndex = new Vector2(0, _slots.Count - 1);
            }
            else
            {
                _slots = new Dictionary<Vector2, BaseSlotController>();
            }
        }

        public override void Select()
        {
            StartCoroutine(AwaitSelect());
        }

        private IEnumerator AwaitSelect()
        {
            yield return null;
            base.Select();
            
            if (_slots.TryGetValue(_currentIndex, out var slot))
                _currentSlot.SetSelected(true);
        }

        public override void UnSelect()
        {
            base.UnSelect();
            _currentSlot.SetSelected(false);
        }

        public override void OnSubmitDown()
        {
            if (!_isSelect)
                return;
            
            if (_currentSlot is LanguageSlotViewModel languageSlotView)
            {
                UnSelect();
                languageSlotView.Click();
            }
        }

        public override void OnCancel()
        {
            UnSelect();
            _menu.Select();

            if (!_isExitUp)
            {
                Activate(false);
                _menu.Activate(true); 
            }
        }
        
        public override void OnSlotIndexChanged(Vector2 direction)
        {
            if (!_isSelect)
                return;
            
            if (direction is { y: 1, x: 0 } && _currentIndex.y == _slots.Count - 1)
            {
                if (_isExitUp)
                {
                    UnSelect();
                    //Activate(false);
                    //_menu.Activate(true);
                    _menu.Select();
                }
            }
            
            base.OnSlotIndexChanged(direction);
        }
    }
}