using System;
using UnityEngine;

namespace Game
{
    [Serializable]
    public class SettingData
    {
        public ReactiveProperty<float> MusicVolume = new();
        public ReactiveProperty<float> SoundVolume = new();
        public ReactiveProperty<bool> IsShowHint = new();

        private float _musicVolume = 1f;
        private float _soundVolume = 1f;
        private bool _isShowHint = false;
        
        public SettingData()
        {
            MusicVolume.Value = _musicVolume;
            SoundVolume.Value = _soundVolume;
            IsShowHint.Value = _isShowHint;
            
            MusicVolume.Changed += MusicVolumeOnChanged;
            SoundVolume.Changed += SoundVolumeOnChanged;
        }

        ~SettingData()
        {
            MusicVolume.Changed -= MusicVolumeOnChanged;
            SoundVolume.Changed -= SoundVolumeOnChanged;
        }

        private void MusicVolumeOnChanged(float value)
        {
            GameData.AudioMixer.SetFloat("MusicVolume", Mathf.Lerp(-80, 0, value));
        }

        private void SoundVolumeOnChanged(float value)
        {
            GameData.AudioMixer.SetFloat("SoundVolume", Mathf.Lerp(-80, 0, value));
        }
    }
}