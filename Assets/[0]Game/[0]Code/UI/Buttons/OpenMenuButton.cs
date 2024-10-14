using UnityEngine;

namespace Game
{
    public class OpenMenuButton : AnimatedButtonOld
    {
        [SerializeField]
        private GameplayMenu _menu;
        
        public override void OnClick()
        {
            _menu.TryShow();
        }
    }
}