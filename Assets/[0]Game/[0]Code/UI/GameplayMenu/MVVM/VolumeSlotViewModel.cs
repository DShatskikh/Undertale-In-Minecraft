using UnityEngine;
using YG;

namespace Game
{
    public abstract class VolumeSlotViewModel : BaseSlotController
    {
        private VolumeSlotView _view;

        public readonly ReactiveProperty<float> Volume = new();
        private bool _isSelect;

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
            
            var horizontal = Input.GetAxisRaw("Horizontal");
            
            if (horizontal != 0)
            {
                Volume.Value += horizontal * Time.deltaTime;
                Volume.Value = Mathf.Clamp(Volume.Value, 0f, 1f);
            }
        }

        public override void SetSelected(bool isSelect)
        {
            _isSelect = isSelect;
            _view.SetSelect(isSelect);
        }

        public abstract void OnSliderChanged(float value);
    }
}