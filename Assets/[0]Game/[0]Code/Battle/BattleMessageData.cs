using System;
using UnityEngine;
using UnityEngine.Localization;

namespace Game
{
    [Serializable]
    public class BattleMessageData
    {
        public LocalizedString LocalizedString;
        
        [Range(0f, 1f)]
        public float Shaking;
    }
}