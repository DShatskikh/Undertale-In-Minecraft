
using System;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

namespace YG
{
    [Serializable]
    public class SavesYG
    {
        // "Технические сохранения" для работы плагина (Не удалять)
        public int idSave;
        public bool isFirstSession = true;
        public string language = "ru";
        public bool promptDone;

        // Ваши сохранения
        public bool IsTutorialComplited;
        public int AdsViews;
        public bool IsSpeakHerobrine;
        public bool IsCapturedWorld;
        public bool IsNotCapturedWorld;
        [FormerlySerializedAs("IsHat")] public bool IsCake;
        public bool IsGoodEnd;
        public bool IsBadEnd;
        public bool IsStrangeEnd;
        public bool IsPalesosEnd;
        public bool IsNotIntroduction;
        public int Palesos;
        public bool IsPrisonKey;
        public bool IsDeveloperKey;
        public bool IsGoldKey;
        public bool IsCheat;
        public bool IsNotFirstPlay;
        public bool IsTelephone;
        public bool IsHerobrineKey;
        [FormerlySerializedAs("IsBuyDonat")] public bool IsBuySupport;
        public bool IsNetherStar;
        public bool IsYouHealthy;

        public int NumberGame = 1;
        //public int MaxHealth = 20;
        [FormerlySerializedAs("GoldLily")] public int GoldTulip = 0;

        public List<IntStringPair> _intPairs = new List<IntStringPair>();

        public bool IsOneOrMoreEnd => IsGoodEnd || IsBadEnd || IsStrangeEnd || IsPalesosEnd;
        public bool IsAllEnd => IsGoodEnd && IsBadEnd && IsStrangeEnd && IsPalesosEnd;
        public string SavedGameData;

        public bool IsAlternativeWorld;

        public int PointIndex;

        public List<string> Companions = new List<string>();

        // Вы можете выполнить какие то действия при загрузке сохранений
        public SavesYG()
        {
            // Допустим, задать значения по умолчанию для отдельных элементов массива
            NumberGame = 1;
            //MaxHealth = 20;
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
