using System;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Game
{
    [Serializable]
    public class Act
    {
        public LocalizedString Name;
        public BattleMessageData Description;
        public BattleMessageData Reaction;

        [Range(-15f, 15f)]
        public int Progress;
        public Sprite Icon;
    }
}