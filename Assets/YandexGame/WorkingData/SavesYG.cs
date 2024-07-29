
using System;
using System.Collections.Generic;
using Game;
using UnityEngine.SceneManagement;

namespace YG
{
    [System.Serializable]
    public class SavesYG
    {
        // "Технические сохранения" для работы плагина (Не удалять)
        public int idSave;
        public bool isFirstSession = true;
        public string language = "ru";
        public bool promptDone;

        // Тестовые сохранения для демо сцены
        // Можно удалить этот код, но тогда удалите и демо (папка Example)
        public int money = 1;                       // Можно задать полям значения по умолчанию
        public string newPlayerName = "Hello!";
        public bool[] openLevels = new bool[3];

        // Ваши сохранения

        public bool IsTutorialComplited;
        public int AdsViews;
        public bool IsSpeakHerobrine;
        public bool IsCapturedWorld;
        public bool IsNotCapturedWorld;
        public bool IsHat;
        public bool IsGoodEnd;
        public bool IsBadEnd;
        public bool IsStrangeEnd;
        public bool IsPalesosEnd;
        public bool IsNotIntroduction;
        public float PositionX;
        public float PositionY;
        public int Palesos;
        public int LocationIndex;
        public int Health;
        public bool IsPrisonKey;
        public bool IsDeveloperKey;
        public bool IsGoldKey;
        public bool IsCheat;
        public bool IsNotFirstPlay;
        public bool IsTelephone;
        public bool IsHerobrineKey;
        public bool IsBuyDonat;

        public int NumberGame = 1;
        public float Volume = 1f;
        public int MaxHealth = 20;

        public List<IntStringPair> _intPairs = new List<IntStringPair>();

        public bool IsOneOrMoreEnd => IsGoodEnd || IsBadEnd || IsStrangeEnd || IsPalesosEnd;
        public bool IsAllEnd => IsGoodEnd && IsBadEnd && IsStrangeEnd && IsPalesosEnd;

        // Поля (сохранения) можно удалять и создавать новые. При обновлении игры сохранения ломаться не должны


        // Вы можете выполнить какие то действия при загрузке сохранений
        public SavesYG()
        {
            // Допустим, задать значения по умолчанию для отдельных элементов массива
            openLevels[1] = true;
            
            NumberGame = 1;
            Volume = GameData.VolumeSlider ? GameData.VolumeSlider.GetCurrentVolume : 1f;
            MaxHealth = 20;
        }

        public void ResetAllIntPair()
        {
            _intPairs = new List<IntStringPair>();
        }
        
        public int GetInt(string key)
        {
            foreach (var pair in _intPairs)
            {
                if (pair.Key == key)
                    return pair.Value;
            }

            return 0;
        }

        public void SetInt(string key, int value)
        {
            for (int i = 0; i < _intPairs.Count; i++)
            {
                if (_intPairs[i].Key == key)
                {
                    _intPairs[i] = new IntStringPair(value, key);
                    return;
                }
            }
            
            _intPairs.Add(new IntStringPair(value, key));
        }

        [Serializable]
        public struct IntStringPair
        {
            public IntStringPair(int value, string key)
            {
                Value = value;
                Key = key;
            }
            
            public int Value;
            public string Key;
        }

        public void FullReset()
        {
            var adsViews = AdsViews;
            
            YandexGame.savesData = new SavesYG();

            YandexGame.savesData.AdsViews = adsViews;
            
            YandexGame.SaveProgress();
            SceneManager.LoadScene(1);
        }
    }
}
