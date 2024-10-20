using UnityEngine;
using YG;

namespace Game
{
    public class SoundVolumeSlotViewModel : VolumeSlotViewModel
    {
        protected override void OnInit()
        {
            YandexGame.savesData.SettingData.SoundVolume.Changed += _view.VolumeOnChanged;
            OnSliderChanged(YandexGame.savesData.SettingData.SoundVolume.Value);
        }

        private void OnDestroy()
        {
            YandexGame.savesData.SettingData.SoundVolume.Changed -= _view.VolumeOnChanged;
        }

        public override void AddVolume(float value)
        {
            YandexGame.savesData.SettingData.SoundVolume.Value = Mathf.Clamp(YandexGame.savesData.SettingData.SoundVolume.Value + value, 0f, 1f);
        }

        public override void OnSliderChanged(float value)
        {
            YandexGame.savesData.SettingData.SoundVolume.Value = value;
            print("OnSliderChanged");
        }
    }
}