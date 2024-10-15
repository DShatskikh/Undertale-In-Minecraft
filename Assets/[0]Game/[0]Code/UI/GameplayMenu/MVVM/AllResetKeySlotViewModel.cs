using UnityEngine;

namespace Game
{
    public class AllResetKeySlotViewModel : BaseSlotController
    {
        private AllResetKeySlotView _view;
        private SettingKeyScreen _settingKeyScreen;

        public void Init(SettingKeyScreen settingKeyScreen)
        {
            _settingKeyScreen = settingKeyScreen;
        }
        
        private void Awake()
        {
            _view = GetComponent<AllResetKeySlotView>();
        }

        private void Start()
        {
            _view.Init(this);
        }

        public override void Select()
        {
            _settingKeyScreen.SelectSlot(this);
        }
        
        public override void Use()
        {
            _settingKeyScreen.OnSubmitUp();
        }
        
        public override void SetSelected(bool isSelect)
        {
            _view.Upgrade(isSelect);
        }
    }
}