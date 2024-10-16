namespace Game
{
    public class InventorySlotViewModel : BaseSlotController
    {
        public ItemConfig Model;
        
        private InventorySlotView _view;
        private bool _isInit;
        private InventoryScreen _inventoryScreen;

        public void Init(InventoryScreen inventoryScreen)
        {
            _view = GetComponent<InventorySlotView>();
            _view.Init(Model, this);
            _inventoryScreen = inventoryScreen;
        }

        public override void SetSelected(bool isSelect)
        {
            _view.Upgrade(isSelect);
        }

        public override void Select()
        {
            _inventoryScreen.SelectSlot(this);
        }

        public void SubmitSlotDown()
        {
            
        }

        public void SubmitSlotUp()
        {
            
        }
    }
}