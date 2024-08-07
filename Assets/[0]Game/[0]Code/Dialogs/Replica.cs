using System;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Tables;
using UnityEngine.Serialization;

namespace Game
{
    [Serializable]
    public struct Replica
    {
        public Sprite Icon;
        public LocalizedString LocalizationString;
    }
}