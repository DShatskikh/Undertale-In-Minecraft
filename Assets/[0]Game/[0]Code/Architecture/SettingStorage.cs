using System;
using System.Collections;
using PixelCrushers;
using UnityEngine;
using UnityEngine.Localization.Settings;

namespace Game
{
    public class SettingStorage : Saver
    {
        public readonly ReactiveProperty<float> MusicVolume = new();
        public readonly ReactiveProperty<float> SoundVolume = new();
        public readonly ReactiveProperty<bool> IsShowHint = new();
        public readonly ReactiveProperty<int> Language = new();

        private Data _saveData = new();

        [Serializable]
        public class Data
        {
            public float MusicVolume = 1f;
            public float SoundVolume = 1f;
            public bool IsShowHint = true;
            public int Language = 0;
        }

        public override void Awake()
        {
            MusicVolume.Changed += MusicVolumeOnChanged;
            SoundVolume.Changed += SoundVolumeOnChanged;
            Language.Changed += LanguageOnChanged;
        }

        public override void OnDestroy()
        {
            MusicVolume.Changed -= MusicVolumeOnChanged;
            SoundVolume.Changed -= SoundVolumeOnChanged;
            Language.Changed -= LanguageOnChanged;
        }

        public override string RecordData()
        {
            return SaveSystem.Serialize(_saveData);
        }

        public override void ApplyData(string s)
        {
            var data = SaveSystem.Deserialize(s, _saveData);
            _saveData ??= data;
            
            MusicVolume.Value = _saveData.MusicVolume;
            SoundVolume.Value = _saveData.SoundVolume;
            IsShowHint.Value = _saveData.IsShowHint;
            Language.Value = _saveData.Language;
        }
        
        public Data GetData() => 
            _saveData;
        
        public void SetData(Data data)
        {
            _saveData = data;
            MusicVolume.Value = data.MusicVolume;
            SoundVolume.Value = data.SoundVolume;
            IsShowHint.Value = data.IsShowHint;
            Language.Value = data.Language;
        }

        private void MusicVolumeOnChanged(float value)
        {
            _saveData.MusicVolume = value;
            GameData.AudioMixer.SetFloat("MusicVolume", Mathf.Lerp(-80, 0, value));
        }

        private void SoundVolumeOnChanged(float value)
        {
            _saveData.SoundVolume = value;
            GameData.AudioMixer.SetFloat("SoundVolume", Mathf.Lerp(-80, 0, value));
        }

        private void LanguageOnChanged(int value)
        {
            _saveData.Language = value;
            GameData.Startup.StartCoroutine(AwaitChangeLanguage(value));
        }
        
        private IEnumerator AwaitChangeLanguage(int id)
        {
            yield return LocalizationSettings.InitializationOperation;
            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[id];
        }
    }
}