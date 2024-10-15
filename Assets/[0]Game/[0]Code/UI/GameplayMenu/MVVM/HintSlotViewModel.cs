using UnityEngine;

namespace Game
{
    public class HintSlotViewModel : BaseSlotController
    {
        private HintSlotView _view;
        private SettingScreen _settingScreen;
        private bool _isSelect;

        public readonly ReactiveProperty<bool> IsToggle = new();

        public void Init(SettingScreen settingScreen)
        {
            _settingScreen = settingScreen;
        }

        private void Awake()
        {
            _view = GetComponent<HintSlotView>();
        }

        private void Start()
        {
            _view.Init(this);
        }

        public override void SetSelected(bool isSelect)
        {
            _isSelect = isSelect;
            _view.SetSelect(isSelect);
        }
        
        public override void Select()
        {
            _settingScreen.SelectSlot(this);
        }
        public override void Use()
        {
            _settingScreen.OnSubmitUp();
        }
    }
}