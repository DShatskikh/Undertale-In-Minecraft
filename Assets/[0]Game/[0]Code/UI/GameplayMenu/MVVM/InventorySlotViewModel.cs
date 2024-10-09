namespace Game
{
    public class InventorySlotViewModel : BaseSlotController
    {
        public InventorySlotModel Model;
        
        private InventorySlotView _view;

        private void Awake()
        {
            _view = GetComponent<InventorySlotView>();
        }

        private void Start()
        {
            _view.Init(Model);
        }

        public override void SetSelected(bool isSelect)
        {
            _view.Upgrade(isSelect, Model);
        }
    }
}