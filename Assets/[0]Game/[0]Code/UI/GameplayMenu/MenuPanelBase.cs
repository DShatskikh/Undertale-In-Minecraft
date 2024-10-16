namespace Game
{
    public class MenuPanelBase : UIPanelBase
    {
        protected bool _isSelect;

        public override void OnSubmitDown() { }

        public override void OnSubmitUp() { }
        public override void OnCancel() { }

        public override void Activate(bool isActive)
        {
            gameObject.SetActive(isActive);
        }

        public virtual void Select()
        {
            if (_isSelect)
                return;
                
            Register(true);
            _isSelect = true;
        }

        public virtual void UnSelect()
        {
            if (!_isSelect)
                return;
            
            Register(false);
            _isSelect = false;
        }
        
        public virtual void SelectSlot(BaseSlotController slotViewModel)
        {
            _currentSlot.SetSelected(false);
            
            foreach (var slot in _slots)
            {
                if (slot.Value == slotViewModel)
                {
                    _currentIndex = slot.Key;
                    break;
                }
            }
            
            _currentSlot.SetSelected(true);
        }
    }
}