namespace Game
{
    public class GuideSlotViewModel : BaseSlotController
    {
        public GuideConfig Model;
        
        private GuideScreen _screen;
        private GuideSlotView _view;

        public void Init(GuideScreen screen)
        {
            _screen = screen;
            _view = GetComponent<GuideSlotView>();
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