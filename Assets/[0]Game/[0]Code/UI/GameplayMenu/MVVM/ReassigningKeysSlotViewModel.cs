using UnityEngine;

namespace Game
{
    public class ReassigningKeysSlotViewModel : BaseSlotController
    {
        [SerializeField]
        private SettingKeyScreen _settingKeyScreen;
        
        [SerializeField]
        private SettingScreen _settingScreen;
        
        private ReassigningKeysSlotView _view;
        private bool _isSelect;
        
        private void Awake()
        {
            _view = GetComponent<ReassigningKeysSlotView>();
        }
        
        private void Start()
        {
            _view.Init(this);
        }

        private void Update()
        {
            if (!_isSelect)
                return;

            if (Input.GetButtonDown("Submit"))
            {
                _settingScreen.UnSelect();
                _settingScreen.Activate(false);
                _settingKeyScreen.Activate(true);
            }
        }

        public override void SetSelected(bool isSelect)
        {
            _isSelect = isSelect;
            _view.SetSelect(isSelect);
        }
    }
}