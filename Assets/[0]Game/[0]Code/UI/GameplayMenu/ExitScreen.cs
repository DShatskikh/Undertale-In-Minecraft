using MoreMountains.Feedbacks;
using UnityEngine;

namespace Game
{
    public class ExitScreen : MenuPanelBase
    {
        [SerializeField]
        private MMF_Player _selectPlayer;
        
        public override void Activate(bool isActive)
        {
            base.Activate(isActive);
            
            if (isActive)
                _selectPlayer.PlayFeedbacks();
        }
        
        public override void OnSubmit()
        {
            
        }

        public override void OnCancel()
        {
            
        }
    }
}