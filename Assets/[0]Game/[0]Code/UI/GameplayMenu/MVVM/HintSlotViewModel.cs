using UnityEngine;
using YG;

namespace Game
{
    public class HintSlotViewModel : BaseSlotController
    {
        private HintSlotView _view;
        private SettingScreen _settingScreen;

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
            _view.IsToggleOnChanged(YandexGame.savesData.SettingData.IsShowHint.Value);
        }

        public override void SetSelected(bool isSelect)
        {
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