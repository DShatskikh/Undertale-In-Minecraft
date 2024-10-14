using UnityEngine;
using YG;

namespace Game
{
    public abstract class VolumeSlotViewModel : BaseSlotController
    {
        [SerializeField]
        private PlusMinusVolumeView _plus, _minus;
        
        private VolumeSlotView _view;

        public readonly ReactiveProperty<float> Volume = new();
        private bool _isSelect;
        private SettingScreen _settingScreen;
        private float _currentHorizontal;

        public void Init(SettingScreen settingScreen)
        {
            _settingScreen = settingScreen;
        }
        
        private void Awake()
        {
            _view = GetComponent<VolumeSlotView>();
        }
        
        private void Start()
        {
            _view.Init(this);
            Volume.Value = YandexGame.savesData.Volume;
        }

        private void Update()
        {
            if (!_isSelect)
                return;
            
            var horizontal = GameData.PlayerInput.actions["Move"].ReadValue<Vector2>().x;
            
            if (horizontal != 0)
            {
                AddVolume(horizontal * Time.deltaTime);

                if (horizontal != _currentHorizontal)
                {
                    if (horizontal > 0)
                    {
                        _plus.OnPointerEnter(null);
                        _plus.OnPointerDown(null);
                    }
                    else if (horizontal < 0)
                    {
                        _minus.OnPointerEnter(null);
                        _minus.OnPointerDown(null);
                    }
                }
            }
            else
            {
                if (_currentHorizontal > 0)
                {
                    _plus.OnPointerExit(null);
                    _plus.OnPointerUp(null);   
                }
                else if (_currentHorizontal < 0)
                {
                    _minus.OnPointerExit(null);
                    _minus.OnPointerUp(null);   
                }
            }

            _currentHorizontal = horizontal;
        }

        public void AddVolume(float value)
        {
            Volume.Value = Mathf.Clamp(Volume.Value + value, 0f, 1f);
        }

        public override void SetSelected(bool isSelect)
        {
            _isSelect = isSelect;
            _view.SetSelect(isSelect);
        }

        public abstract void OnSliderChanged(float value);
        
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
            _view.SubmitDown();
        }

        public override void SubmitUp()
        {
            _view.SubmitUp();
        }
    }
}