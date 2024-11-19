using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Localization.Settings;

namespace Game
{
    [Serializable]
    public class SettingData
    {
        public ReactiveProperty<float> MusicVolume = new();
        public ReactiveProperty<float> SoundVolume = new();
        public ReactiveProperty<bool> IsShowHint = new();
        public ReactiveProperty<int> Language = new();

        private float _musicVolume = 1f;
        private float _soundVolume = 1f;
        private bool _isShowHint = false;
        private int _language = 0;
        
        public SettingData()
        {
            MusicVolume.Value = _musicVolume;
            SoundVolume.Value = _soundVolume;
            IsShowHint.Value = _isShowHint;
            Language.Value = _language;
            
            MusicVolume.Changed += MusicVolumeOnChanged;
            SoundVolume.Changed += SoundVolumeOnChanged;
            Language.Changed += LanguageOnChanged;
        }

        ~SettingData()
        {
            MusicVolume.Changed -= MusicVolumeOnChanged;
            SoundVolume.Changed -= SoundVolumeOnChanged;
            Language.Changed -= LanguageOnChanged;
        }

        private void MusicVolumeOnChanged(float value)
        {
            _musicVolume = value;
            GameData.AudioMixer.SetFloat("MusicVolume", Mathf.Lerp(-80, 0, value));
        }

        private void SoundVolumeOnChanged(float value)
        {
            _soundVolume = value;
            GameData.AudioMixer.SetFloat("SoundVolume", Mathf.Lerp(-80, 0, value));
        }

        private void LanguageOnChanged(int value)
        {
            _language = value;
            GameData.Startup.StartCoroutine(AwaitChangeLanguage(value));
        }
        
        private IEnumerator AwaitChangeLanguage(int id)
        {
            yield return LocalizationSettings.InitializationOperation;
            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[id];
            
            Debug.Log($"id: {id}");
        }
    }
}