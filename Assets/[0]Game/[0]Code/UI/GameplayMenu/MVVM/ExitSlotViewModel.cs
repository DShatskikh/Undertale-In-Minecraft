namespace Game
{
    public class ExitSlotViewModel : BaseSlotController
    {
        public ExitSlotConfig Model;
        
        private ExitSlotView _view;
        private ExitScreen _screen;

        public void Init(ExitScreen screen)
        {
            _screen = screen;
            _view = GetComponent<ExitSlotView>();
            _view.Init(Model, this);
        }

        public override void SetSelected(bool isSelect)
        {
            _view.Upgrade(isSelect);
        }

        public override void Select()
        {
            _screen.SelectSlot(this);
        }
        
        public void SubmitSlotDown()
        {
            
        }

        public void SubmitSlotUp()
        {
            
        }
    }
}