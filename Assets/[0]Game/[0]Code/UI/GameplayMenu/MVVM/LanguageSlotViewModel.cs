using YG;

namespace Game
{
    public class LanguageSlotViewModel : BaseSlotController
    {
        private SettingScreen _settingScreen;
        private LanguageSlotView _view;

        public void Init(SettingScreen settingScreen)
        {
            _settingScreen = settingScreen;
        }

        private void Awake()
        {
            _view = GetComponent<LanguageSlotView>();
        }

        private void Start()
        {
            _view.Init(this);
        }

        public override void SetSelected(bool isSelect)
        {
            _view.Upgrade(isSelect, false);
        }

        public void Click()
        {
            GameData.EffectSoundPlayer.Play(GameData.AssetProvider.ClickSound);

            if (YandexGame.savesData.SettingData.Language.Value == 0)
                YandexGame.savesData.SettingData.Language.Value = 1;
            else
                YandexGame.savesData.SettingData.Language.Value = 0;
        }

        public override void Select()
        {
            _settingScreen.SelectSlot(this);
        }

        public override void Use()
        {
            _settingScreen.OnSubmitUp();
        }

        public override void SubmitDown()
        {
            //_view.SubmitDown();
        }

        public override void SubmitUp()
        {
            //_view.SubmitUp();
            Click();
        }
    }
}