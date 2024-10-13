namespace Game
{
    public class InventorySlotViewModel : BaseSlotController
    {
        public ItemConfig Model;
        
        private InventorySlotView _view;
        private bool _isInit;
        
        private void Awake()
        {
            _view = GetComponent<InventorySlotView>();
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