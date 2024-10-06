using UnityEngine;
using UnityEngine.Localization;

namespace Game
{
    public abstract class ActBase : ScriptableObject
    {
        public LocalizedString Name;
        public abstract void Use();
    }
}