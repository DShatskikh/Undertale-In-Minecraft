using UnityEngine;
using YG;

namespace Game
{
    public class SoundVolumeSlotViewModel : VolumeSlotViewModel
    {
        protected override void OnInit()
        {
            GameData.SettingStorage.SoundVolume.Changed += _view.VolumeOnChanged;
            OnSliderChanged(GameData.SettingStorage.SoundVolume.Value);
        }

        private void OnDestroy()
        {
            GameData.SettingStorage.SoundVolume.Changed -= _view.VolumeOnChanged;
        }

        public override void AddVolume(float value)
        {
            GameData.SettingStorage.SoundVolume.Value = Mathf.Clamp(GameData.SettingStorage.SoundVolume.Value + value, 0f, 1f);
        }

        public override void OnSliderChanged(float value)
        {
            GameData.SettingStorage.SoundVolume.Value = value;
        }
    }
}