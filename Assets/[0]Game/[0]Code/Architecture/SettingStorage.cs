using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Localization.Settings;
using YG;

namespace Game
{
    public class SettingStorage
    {
        public readonly ReactiveProperty<float> MusicVolume = new();
        public readonly ReactiveProperty<float> SoundVolume = new();
        public readonly ReactiveProperty<bool> IsShowHint = new();
        public readonly ReactiveProperty<int> Language = new();

        private bool _isInit;
        
        public SettingStorage()
        {
            var settingData = GetData();

            MusicVolume.Changed += MusicVolumeOnChanged;
            SoundVolume.Changed += SoundVolumeOnChanged;
            Language.Changed += LanguageOnChanged;
            
            MusicVolume.Value = settingData.MusicVolume;
            SoundVolume.Value = settingData.SoundVolume;
            IsShowHint.Value = settingData.IsShowHint;
            Language.Value = settingData.Language;

            _isInit = true;
        }

        ~SettingStorage()
        {
            MusicVolume.Changed -= MusicVolumeOnChanged;
            SoundVolume.Changed -= SoundVolumeOnChanged;
            Language.Changed -= LanguageOnChanged;
        }

        private void MusicVolumeOnChanged(float value)
        {
            var settingData = GetData();
            settingData.MusicVolume = value;
            GameData.AudioMixer.SetFloat("MusicVolume", Mathf.Lerp(-80, 0, value));
            Save(settingData);
        }

        private void SoundVolumeOnChanged(float value)
        {
            var settingData = GetData();
            settingData.SoundVolume = value;
            GameData.AudioMixer.SetFloat("SoundVolume", Mathf.Lerp(-80, 0, value));
            Save(settingData);
        }

        private void LanguageOnChanged(int value)
        {
            var settingData = GetData();
            settingData.Language = value;
            GameData.Startup.StartCoroutine(AwaitChangeLanguage(value));

            Save(settingData);
        }
        
        private IEnumerator AwaitChangeLanguage(int id)
        {
            yield return LocalizationSettings.InitializationOperation;
            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[id];
        }

        private void Save(SettingData settingData)
        {
            if (!_isInit)
                return;
            
            YandexGame.savesData.SettingDataJson = JsonUtility.ToJson(settingData);
            YandexGame.SaveProgress();
        }

        private SettingData GetData()
        {
            return JsonUtility.FromJson<SettingData>(YandexGame.savesData.SettingDataJson);
        }
    }
}