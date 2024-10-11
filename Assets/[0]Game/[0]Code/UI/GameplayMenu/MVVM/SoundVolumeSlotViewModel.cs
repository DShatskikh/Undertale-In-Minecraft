using UnityEngine;
using YG;

namespace Game
{
    public class SoundVolumeSlotViewModel : VolumeSlotViewModel
    {
        public override void OnSliderChanged(float value)
        {
            Volume.Value = value;
            YandexGame.savesData.Volume = value;
            GameData.Mixer.audioMixer.SetFloat("MasterVolume", Mathf.Lerp(-80, 0, value));
        }
    }
}