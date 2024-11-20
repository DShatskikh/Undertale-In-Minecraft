using System;

namespace Game
{
    [Serializable]
    public struct SettingData
    {
        public float MusicVolume;
        public float SoundVolume;
        public bool IsShowHint;
        public int Language;

        public SettingData(float musicVolume, float soundVolume, bool isShowHint, int language)
        {
            MusicVolume = musicVolume;
            SoundVolume = soundVolume;
            IsShowHint = isShowHint;
            Language = language;
        }
    }
}