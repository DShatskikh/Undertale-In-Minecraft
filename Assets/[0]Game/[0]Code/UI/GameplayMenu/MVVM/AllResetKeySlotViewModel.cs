namespace Game
{
    public class AllResetKeySlotViewModel : BaseSlotController
    {
        private AllResetKeySlotView _view;

        private void Awake()
        {
            _view = GetComponent<AllResetKeySlotView>();
        }

        private void Start()
        {
            _view.Init();
        }

        public override void SetSelected(bool isSelect)
        {
            _view.Upgrade(isSelect);
        }
    }
}