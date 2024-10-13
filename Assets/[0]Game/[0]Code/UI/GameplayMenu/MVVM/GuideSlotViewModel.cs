namespace Game
{
    public class GuideSlotViewModel : BaseSlotController
    {
        public GuideConfig Model;
        
        private GuideSlotView _view;
        private bool _isInit;
        
        private void Awake()
        {
            _view = GetComponent<GuideSlotView>();
        }

        private void Start()
        {
            if (!_isInit)
                _view.Init(Model);
        }

        public override void SetSelected(bool isSelect)
        {
            if (!_isInit)
            {
                _isInit = true;
                _view.Init(Model);
            }
            
            _view.Upgrade(isSelect);
        }
    }
}