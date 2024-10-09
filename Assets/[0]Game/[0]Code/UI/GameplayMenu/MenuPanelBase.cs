using UnityEngine;

namespace Game
{
    public class MenuPanelBase : UIPanelBase
    {
        protected bool _isSelect;
        
        public override void OnSubmit() { throw new System.NotImplementedException(); }

        public override void OnCancel() { throw new System.NotImplementedException(); }

        public virtual void Select()
        {
            _isSelect = true;
        }

        public virtual void UnSelect()
        {
            _isSelect = false;
        }
    }
}