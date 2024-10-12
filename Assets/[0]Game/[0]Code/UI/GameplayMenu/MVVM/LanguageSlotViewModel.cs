namespace Game
{
    public class LanguageSlotViewModel : BaseSlotController
    {
        private LanguageSlotView _view;

        private void Awake()
        {
            _view = GetComponent<LanguageSlotView>();
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