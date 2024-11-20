using System;
using UnityEngine;
using YG;

namespace Game
{
    public class MusicVolumeSlotViewModel : VolumeSlotViewModel
    {
        protected override void OnInit()
        {
            GameData.SettingStorage.MusicVolume.Changed += _view.VolumeOnChanged;
            OnSliderChanged(GameData.SettingStorage.MusicVolume.Value);
        }

        private void OnDestroy()
        {
            GameData.SettingStorage.MusicVolume.Changed -= _view.VolumeOnChanged;
        }

        public override void AddVolume(float value)
        {
            GameData.SettingStorage.MusicVolume.Value = Mathf.Clamp(GameData.SettingStorage.MusicVolume.Value + value, 0f, 1f);
        }

        public override void OnSliderChanged(float value)
        {
            GameData.SettingStorage.MusicVolume.Value = value;
        }
    }
}