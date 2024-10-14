namespace Game
{
    public class MenuSlotViewModel : BaseSlotController
    {
        public MenuSlotConfig Model;
        
        private MenuSlotView _view;
        private bool _isInit;
        private MainMenu _mainMenu;

        public void Init(MainMenu mainMenu)
        {
            _mainMenu = mainMenu;
        }
        
        public override void SetSelected(bool isSelect)
        {
            if (!_isInit)
            {
                _view = GetComponent<MenuSlotView>();
                _view.Init(Model, this);
                _isInit = true;
            }
            
            _view.Upgrade(isSelect);
        }

        public override void Select()
        {
            _mainMenu.SelectSlot(this);
        }

        public override void Use()
        {
            _mainMenu.OnSubmitUp();
        }

        public override void SubmitDown()
        {
            _view.SubmitDown();
        }

        public override void SubmitUp()
        {
            _view.SubmitUp();
        }
    }
}