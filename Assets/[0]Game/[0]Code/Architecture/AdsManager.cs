using System;
using PixelCrushers.DialogueSystem;
using UnityEngine;
using UnityEngine.Events;
using YG;

namespace Game
{
    public class AdsManager : MonoBehaviour
    {
        [SerializeField]
        private AdsPair[] _pairs;

        private void Start()
        {
            YandexGame.RewardVideoEvent += RewardVideoEvent;
            Lua.RegisterFunction(nameof(ShowRewardVideo), this, SymbolExtensions.GetMethodInfo(() => ShowRewardVideo(0)));
        }

        private void OnDestroy()
        {
            YandexGame.RewardVideoEvent -= RewardVideoEvent;
            Lua.UnregisterFunction(nameof(ShowRewardVideo));
        }

        private void ShowRewardVideo(double value)
        {
            var index = (int)value;
            
            if (YandexGame.savesData.IsBuySupport)
            {
                RewardVideoEvent(index);
                return;
            }

            YandexGame.RewVideoShow(1);
        }
        
        private void RewardVideoEvent(int value)
        {
            YandexGame.RewardVideoEvent = null;
            YandexGame.savesData.AdsViews += 1;

            var adsType = (AdsType)value;

            foreach (var pair in _pairs)
            {
                if (pair.AdsType == adsType)
                {
                    switch (adsType)
                    {
                        case AdsType.Other:
                            pair.Event.Invoke();
                            break;
                        case AdsType.BuyMask:
                            pair.Event.Invoke();
                            break;
                        default:
                            Debug.LogError("Не то число ????");
                            break;
                    }
                }
            }
        }

        private enum AdsType
        {
            Other,
            BuyMask
        }
        
        [Serializable]
        private class AdsPair
        {
            public AdsType AdsType;
            public UnityEvent Event;
        }
    }
}