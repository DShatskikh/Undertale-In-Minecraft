using System.Collections.Generic;
using MoreMountains.Feedbacks;
using UnityEngine;

namespace Game
{
    public class SettingKeyScreen : MenuPanelBase
    {
        [SerializeField]
        private MMF_Player _selectPlayer;
        
        [SerializeField]
        private SettingScreen _settingScreen;
        
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
            _settingScreen.Activate(true);
            _settingScreen.Select();
        }

        public override void OnSubmit()
        {
            
        }

        public override void OnCancel()
        {
            Activate(false);
            UnSelect();
        }
    }
}