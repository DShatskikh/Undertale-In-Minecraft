using System;
using UnityEngine;
using UnityEngine.Localization;

namespace Game
{
    [Serializable]
    public class Act
    {
        public LocalizedString Name;
        public BattleMessageData Reaction;
        
        [Range(-15f, 15f)]
        public int Progress;
    }
}