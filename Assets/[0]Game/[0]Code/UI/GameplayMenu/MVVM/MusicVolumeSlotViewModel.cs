using UnityEngine;
using YG;

namespace Game
{
    public class MusicVolumeSlotViewModel : VolumeSlotViewModel
    {
        public override void OnSliderChanged(float value)
        {
            Volume.Value = value;
            YandexGame.savesData.Volume = value;
            GameData.Mixer.audioMixer.SetFloat("MasterVolume", Mathf.Lerp(-80, 0, value));
        }
    }
}