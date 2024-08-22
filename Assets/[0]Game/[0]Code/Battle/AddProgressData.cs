using System;
using UnityEngine;
using UnityEngine.Localization;

namespace Game
{
    [Serializable]
    public class AddProgressData
    {
        public Color MoreColor, LessColor;
        public LocalizedString MoreEpic, More, Less, LessEpic;
        public AudioClip MoreSound, LessSound;
    }
}