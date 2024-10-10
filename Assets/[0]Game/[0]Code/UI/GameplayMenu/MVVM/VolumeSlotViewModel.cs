using System.Collections.Generic;
using UnityEngine;
using YG;

namespace Game
{
    public class VolumeSlotViewModel : BaseSlotController
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

        private void OnDestroy()
        {
            
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

        public void OnSliderChanged(float value)
        {
            Volume.Value = value;
            YandexGame.savesData.Volume = value;
            GameData.Mixer.audioMixer.SetFloat("MasterVolume", Mathf.Lerp(-80, 0, value));
        }
    }
}