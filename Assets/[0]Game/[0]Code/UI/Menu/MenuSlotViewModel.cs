namespace Game
{
    public class MenuSlotViewModel : BaseSlotController
    {
        public MenuSlotConfig Model;
        
        private MenuSlotView _view;
        private bool _isInit;

        public override void SetSelected(bool isSelect)
        {
            if (!_isInit)
            {
                _view = GetComponent<MenuSlotView>();
                _view.Init(Model);
                _isInit = true;
            }
            
            _view.Upgrade(isSelect);
        }
    }
}