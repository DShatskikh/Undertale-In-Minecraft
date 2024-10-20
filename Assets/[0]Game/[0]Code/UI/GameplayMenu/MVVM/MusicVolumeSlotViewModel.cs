using System;
using UnityEngine;
using YG;

namespace Game
{
    public class MusicVolumeSlotViewModel : VolumeSlotViewModel
    {
        protected override void OnInit()
        {
            YandexGame.savesData.SettingData.MusicVolume.Changed += _view.VolumeOnChanged;
            OnSliderChanged(YandexGame.savesData.SettingData.MusicVolume.Value);
        }

        private void OnDestroy()
        {
            YandexGame.savesData.SettingData.MusicVolume.Changed -= _view.VolumeOnChanged;
        }

        public override void AddVolume(float value)
        {
            YandexGame.savesData.SettingData.MusicVolume.Value = Mathf.Clamp(YandexGame.savesData.SettingData.MusicVolume.Value + value, 0f, 1f);
        }

        public override void OnSliderChanged(float value)
        {
            YandexGame.savesData.SettingData.MusicVolume.Value = value;
            print("OnSliderChanged");
        }
    }
}