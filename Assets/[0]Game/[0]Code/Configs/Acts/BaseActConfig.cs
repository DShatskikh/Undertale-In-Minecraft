using UnityEngine;
using UnityEngine.Localization;

namespace Game
{
    public abstract class BaseActConfig : ScriptableObject
    {
        public LocalizedString Name;
        public abstract Sprite GetIcon();
        public abstract int GetProgress();
        public abstract void Use();
    }
}