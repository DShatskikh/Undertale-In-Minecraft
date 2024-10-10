namespace Game
{
    public class ExitSlotViewModel : BaseSlotController
    {
        public ExitSlotConfig Model;
        
        private ExitSlotView _view;

        private void Awake()
        {
            _view = GetComponent<ExitSlotView>();
        }

        private void Start()
        {
            _view.Init(Model);
        }

        public override void SetSelected(bool isSelect)
        {
            _view.Upgrade(isSelect);
        }
    }
}