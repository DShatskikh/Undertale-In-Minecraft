using UnityEngine;

namespace Game
{
    public class ReassigningKeysSlotViewModel : BaseSlotController
    {
        [SerializeField]
        private SettingKeyScreen _settingKeyScreen;
        
        private SettingScreen _settingScreen;
        private ReassigningKeysSlotView _view;
        private bool _isSelect;

        public void Init(SettingScreen settingScreen)
        {
            _settingScreen = settingScreen;
        }

        private void Awake()
        {
            _view = GetComponent<ReassigningKeysSlotView>();
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

        public void Click()
        {
            print("ReassigningKeysSlotViewModel Click");
            _settingScreen.UnSelect();
            _settingScreen.Activate(false);
            _settingKeyScreen.Activate(true);
        }
    }
}